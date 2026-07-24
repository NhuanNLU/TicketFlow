using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Notification: EntityAuditBase<Guid>
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public required int Type { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}