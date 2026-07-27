using TicketFlow.Service.Models;
namespace TicketFlow.API.Middleware;

public class ExceptionHandlingMiddleware: IMiddleware
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(IHostEnvironment environment, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e,e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        // 1. AN TOÀN: Nếu response đã bắt đầu gửi về Client, không can thiệp thêm để tránh crash
        if (context.Response.HasStarted)
        {
            _logger.LogWarning("The response has already started, exception middleware will not write response.");
            return;
        }
        var statusCode = GetStatusCode(e);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        var response = ApiResponseFactory.ErrorResponse(
            message: ResolveClientMessage(e, statusCode),
            errors: _environment.IsDevelopment() ? new { detail = e.Message } : null,
            traceId: context.TraceIdentifier);
        await context.Response.WriteAsJsonAsync(response);
    }

    private static int GetStatusCode(Exception e) =>
        e switch
        {
            ArgumentException or InvalidOperationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    private static string ResolveClientMessage(Exception e, int statusCode)
        => statusCode >= 500 ? "An unexpected error occurred" : e.Message;
}