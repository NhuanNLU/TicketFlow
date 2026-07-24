using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Venue: EntityAuditSoftDeleteBase<Guid>
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string MapUrl { get; set; }
    public string? Description { get; set; }
    public int? TotalCapacity { get; set; }
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<Zone> Zones { get; set; } = new List<Zone>();
}