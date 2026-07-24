using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Staff: EntityAuditSoftDeleteBase<Guid>
{
    public Organizer Organizer { get; set; }
    public Guid OrganizerId { get; set; }
    public required string Username { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Avatar { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }
    public int Status  { get; set; }
}