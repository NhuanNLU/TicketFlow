using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class AuditLog: EntityAuditBase<Guid>
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public required string Action { get; set; }         // Create, Update, Delete, Login,...
    public required string EntityName { get; set; }     // "Ticket", "User", "Event",...
    public Guid EntityId { get; set; }                  // Id của bản ghi bị thay đổi
    public string? OldValues { get; set; }              // JSON trước khi thay đổi
    public string? NewValues { get; set; }              // JSON sau khi thay đổi
}