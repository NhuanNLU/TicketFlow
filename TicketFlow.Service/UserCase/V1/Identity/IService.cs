namespace TicketFlow.Service.UserCase.V1.Identity;

public interface IService
{
    public Task Register(Request.RegisterRequest request, CancellationToken cancellationToken);
    public Task<Response.LoginResponse> Login(Request.LoginRequest request, CancellationToken cancellationToken);
}