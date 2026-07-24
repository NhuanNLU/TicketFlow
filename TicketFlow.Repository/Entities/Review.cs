using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Review: EntityBase<Guid>
{
    public Customer Customer { get; set; }
    public Guid CustomerId { get; set; }
    public Organizer Organizer { get; set; }
    public Guid OrganizerId { get; set; }
    public Event? Event { get; set; }
    public Guid? EventId { get; set; }
    public required int Rating { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}