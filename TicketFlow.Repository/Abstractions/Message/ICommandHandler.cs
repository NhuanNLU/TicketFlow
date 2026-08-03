using MediatR;
using TicketFlow.Repository.Abstractions.Shared;

namespace TicketFlow.Repository.Abstractions.Message;
public interface ICommandHandler<TCommand>: IRequestHandler<TCommand, Result> 
    where TCommand: ICommand { }
public interface ICommandHandler<TCommand, TResponse>: IRequestHandler<TCommand, Result<TResponse>>
    where TCommand: ICommand<TResponse>{}