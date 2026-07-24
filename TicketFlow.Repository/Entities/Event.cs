using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Event: EntityAuditSoftDeleteBase<Guid>
{
    public Organizer Organizer { get; set; }
    public Guid OrganizerId { get; set; }
    public Venue Venue { get; set; }
    public Guid VenueId { get; set; }
    public required string Name { get; set; }
    public int Status { get; set; }
    public required DateTimeOffset DateOfEvent { get; set; }
    public string? Description { get; set; }               // 👈 thêm: mô tả sự kiện
    public string? CoverImageUrl { get; set; }             // 👈 thêm: ảnh bìa
    public int Category { get; set; }                      // 👈 thêm: Concert, Theatre, Sports, Conference
    public DateTimeOffset? SaleStartDate { get; set; }     // 👈 thêm: mở bán từ ngày
    public DateTimeOffset? SaleEndDate { get; set; }       // 👈 thêm: kết thúc bán
    public int MaxTicketsPerUser { get; set; } = 4;        // 👈 thêm: tối đa vé/user
    public ICollection<IdolEvent> IdolEvents { get; set; } =  new List<IdolEvent>();
    
}