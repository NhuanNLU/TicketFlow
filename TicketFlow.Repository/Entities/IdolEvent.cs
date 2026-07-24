using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class IdolEvent: EntityBase<Guid>
{
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public Idol Idol { get; set; }
    public Guid IdolId { get; set; }
    public int? Order { get; set; }          // Thứ tự biểu diễn
    public TimeSpan? PerformanceTime { get; set; } // Giờ biểu diễn
}