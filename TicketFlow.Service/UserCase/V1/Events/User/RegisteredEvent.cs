using TicketFlow.Repository.Abstractions.Message;

namespace TicketFlow.Service.UserCase.V1.Events.User;

public class RegisteredEvent: IDomainEvent
{
    public Guid Id { get; init; }
    public string Email { get; set; }
    public string UserName { get; set; }
    
}