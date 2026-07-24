using TicketFlow.Repository.Abstractions.Entities;

namespace TicketFlow.Repository.Abstractions;

public abstract class EntitySoftDeleteBase<TKey>: IEntityBase<TKey>, ISoftDelete
{
    public TKey Id { get; set; }
    public bool IsDeleted { get; set; }
}