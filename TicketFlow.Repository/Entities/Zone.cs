using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Zone: EntityAuditSoftDeleteBase<Guid>
{
    public Venue Venue { get; set; }
    public Guid VenueId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Capacity { get; set; } //số ghế trong zone
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}