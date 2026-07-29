using MediatR;

namespace TicketFlow.Repository.Abstractions.Message;

public interface IDomainEventHandler<TEvent>: INotificationHandler<TEvent> where TEvent: IDomainEvent { }