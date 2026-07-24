using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Report: EntityAuditBase<Guid>
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public int Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsRead { get; set; }
    public Guid TargetId { get; set; }       //   Id đối tượng bị report
    public int TargetType { get; set; }      //    Loại đối tượng bị report (User, Ticket, ...)
}