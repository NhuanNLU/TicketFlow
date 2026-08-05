using Microsoft.AspNetCore.Mvc;
using TicketFlow.Service.Models;
using TicketFlow.Service.UserCase.V1.Identity;

namespace TicketFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class IdentityController: ControllerBase
{
    private readonly IService _service;
    public IdentityController(IService service)
    {
        _service = service;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(Request.RegisterRequest request, CancellationToken cancellationToken = default)
    {
        await _service.Register(request, cancellationToken);
        return Ok(ApiResponseFactory.SuccessResponse("Register Success", null, HttpContext.TraceIdentifier));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(Request.LoginRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _service.Login(request, cancellationToken);
        return Ok(ApiResponseFactory.SuccessResponse("Login Success", result, HttpContext.TraceIdentifier));
    }
}