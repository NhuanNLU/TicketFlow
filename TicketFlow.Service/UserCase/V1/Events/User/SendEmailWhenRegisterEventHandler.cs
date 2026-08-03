using TicketFlow.Repository.Abstractions.Message;

namespace TicketFlow.Service.UserCase.V1.Events.User;

public class SendEmailWhenRegisterEventHandler: IDomainEventHandler<RegisteredEvent>
{
    public Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}