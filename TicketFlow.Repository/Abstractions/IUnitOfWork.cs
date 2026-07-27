namespace TicketFlow.Repository.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    //Task RollbackAsync(CancellationToken cancellationToken = default);
}