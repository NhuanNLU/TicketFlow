using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Organizer: EntityAuditSoftDeleteBase<Guid>
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public required string OrganizerName { get; set; } 
    public required string OrganizerEmail { get; set; }
    public required string OrganizerPhone { get; set; }
    public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}