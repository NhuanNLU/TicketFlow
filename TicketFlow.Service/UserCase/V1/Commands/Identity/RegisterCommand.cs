using TicketFlow.Repository.Abstractions.Message;

namespace TicketFlow.Service.UserCase.V1.Commands.Identity;
public class RegisterCommand: ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}