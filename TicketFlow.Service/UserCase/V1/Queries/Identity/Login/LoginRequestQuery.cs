using TicketFlow.Repository.Abstractions.Message;

namespace TicketFlow.Service.UserCase.V1.Queries.Identity.Login;

public class LoginRequestQuery: ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}