using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class EventZone: EntityAuditSoftDeleteBase<Guid>
{
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public Zone Zone { get; set; }
    public Guid ZoneId { get; set; }
    public required decimal Price { get; set; }
}