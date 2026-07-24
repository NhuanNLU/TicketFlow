using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Customer: EntityAuditSoftDeleteBase<Guid>
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}