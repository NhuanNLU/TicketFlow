using MediatR;

namespace TicketFlow.Repository.Abstractions.Message;

public interface ICommand: IRequest { }
public interface ICommand<out TResponse>: IRequest<TResponse>{}