using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Seat: EntityAuditSoftDeleteBase<Guid>
{
    public Zone Zone { get; set; }
    public Guid ZoneId { get; set; }
    public required string Row { get; set; }
    public int Number { get; set; }
    public int SeatType { get; set; }
}