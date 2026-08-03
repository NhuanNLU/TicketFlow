using TicketFlow.Repository.Abstractions.Entities;

namespace TicketFlow.Repository.Abstractions;

public abstract class EntityAuditSoftDeleteBase<TKey>: IEntityAuditBase<TKey>, ISoftDelete
{
    public TKey Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}