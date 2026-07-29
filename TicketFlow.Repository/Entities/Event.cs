using TicketFlow.Repository.Abstractions;
using TicketFlow.Repository.Enum.Event;

namespace TicketFlow.Repository.Entities;

public class Event: EntityAuditSoftDeleteBase<Guid>
{
    public Organizer Organizer { get; set; }
    public Guid OrganizerId { get; set; }
    public Venue Venue { get; set; }
    public Guid VenueId { get; set; }
    public required string Name { get; set; }
    public EventStatusEnum Status { get; set; }
    public required DateTimeOffset DateOfEvent { get; set; }
    public string? Description { get; set; }               // 👈 thêm: mô tả sự kiện
    public string? CoverImageUrl { get; set; }             // 👈 thêm: ảnh bìa
    public EventCategoryEnum Category { get; set; }                      // 👈 thêm: Concert, Theatre, Sports, Conference
    public DateTimeOffset? SaleStartDate { get; set; }     // 👈 thêm: mở bán từ ngày
    public DateTimeOffset? SaleEndDate { get; set; }       // 👈 thêm: kết thúc bán
    public int MaxTicketsPerUser { get; set; } = 4;        // 👈 thêm: tối đa vé/user
    public ICollection<IdolEvent> IdolEvents { get; set; } =  new List<IdolEvent>();
    public ICollection<EventZone> EventZones { get; set; } = new List<EventZone>();
    public ICollection<EventSeat>  EventSeats { get; set; } = new List<EventSeat>();
}