using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class User: EntityAuditSoftDeleteBase<Guid>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Avatar { get; set; }
    public int? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Bio { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
    public int Role  { get; set; }
    public int Status  { get; set; }
    public bool EmailConfirmed  { get; set; }
    public DateTimeOffset? LastLoginAt   { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime  { get; set; }
    public Customer? Customer  { get; set; }
    public Organizer? Organizer { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Report> Reports { get; set; } = new List<Report>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}