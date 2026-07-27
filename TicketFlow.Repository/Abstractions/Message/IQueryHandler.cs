using MediatR;

namespace TicketFlow.Repository.Abstractions.Message;

public interface IQueryHandler<in TQuery, TResponse>: IRequestHandler<TQuery, TResponse> where TQuery: IQuery<TResponse> { }