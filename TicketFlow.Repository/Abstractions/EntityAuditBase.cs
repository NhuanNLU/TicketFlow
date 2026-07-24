using TicketFlow.Repository.Abstractions.Entities;

namespace TicketFlow.Repository.Abstractions;

public abstract class EntityAuditBase<TKey>: IEntityAuditBase<TKey>
{
    public TKey Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
}