using MediatR;
using TicketFlow.Repository.Abstractions.Shared;

namespace TicketFlow.Repository.Abstractions.Message;

public interface ICommand: IRequest<Result> { }
public interface ICommand<TResponse>: IRequest<Result<TResponse>> {}