using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Campaign: EntityAuditSoftDeleteBase<Guid>
{
    public Organizer? Organizer { get; set; }
    public Guid? OrganizerId { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int DiscountType { get; set; }                  // 👈 thêm: Percentage / FixedAmount
    public decimal DiscountValue { get; set; }             // 👈 thêm: 50 (nghĩa là 50% hoặc 50k)
    public decimal? MaxDiscount { get; set; }              // 👈 thêm: giới hạn tối đa (VD: 100k)
    public decimal? MinOrderAmount { get; set; }           // 👈 thêm: đơn tối thiểu
    public DateTimeOffset StartDate { get; set; }          // 👈 thêm: ngày bắt đầu
    public DateTimeOffset EndDate { get; set; }            // 👈 thêm: ngày kết thúc
    public int UsageLimit { get; set; }                    // 👈 thêm: tổng số lần dùng được
    public int UsagePerUser { get; set; }                  // 👈 thêm: mỗi user dùng mấy lần
    public int UsedCount { get; set; }                     // 👈 thay cho Quantity: số lần đã dùng
    public int Status { get; set; }
    public ICollection<BookingCampaign> BookingCampaigns { get; set; } = new List<BookingCampaign>();
}