using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class EventSeat: EntityAuditSoftDeleteBase<Guid>
{
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public Seat Seat { get; set; }
    public Guid SeatId { get; set; }
    public int Status { get; set; }
    public DateTimeOffset? LockExpiry  { get; set; }//Thời gian hết hạn giữ chỗ tạm thời  
}