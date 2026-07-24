using TicketFlow.Repository.Abstractions.Entities;

namespace TicketFlow.Repository.Abstractions;

public abstract class EntityBase<TKey>: IEntityBase<TKey>
{
    public TKey Id { get; set; }
}