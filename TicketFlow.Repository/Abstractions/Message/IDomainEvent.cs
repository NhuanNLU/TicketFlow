using MediatR;

namespace TicketFlow.Repository.Abstractions.Message;

public interface IDomainEvent: INotification
{
    public Guid Id { get; init; }
}