using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);

    // public Task RollbackAsync(CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
}