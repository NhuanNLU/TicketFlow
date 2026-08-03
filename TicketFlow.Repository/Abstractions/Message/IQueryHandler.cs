using MediatR;
using TicketFlow.Repository.Abstractions.Shared;

namespace TicketFlow.Repository.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse>: IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery: IQuery<TResponse> { }