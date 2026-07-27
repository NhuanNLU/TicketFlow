using MediatR;

namespace TicketFlow.Repository.Abstractions.Message;

public interface IQuery<TResponse>: IRequest<TResponse> { }