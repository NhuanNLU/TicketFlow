using MediatR;
using TicketFlow.Repository.Abstractions.Shared;

namespace TicketFlow.Repository.Abstractions.Message;

public interface IQuery<TResponse>: IRequest<Result<TResponse>> { }