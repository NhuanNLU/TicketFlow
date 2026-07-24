using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class CampaignEvent: EntitySoftDeleteBase<Guid>
{
    public Campaign Campaign { get; set; }
    public Guid CampaignId { get; set; }
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public decimal? DiscountOverride { get; set; }  // Giảm giá riêng cho event này
    public bool IsActive { get; set; } = true;      // Bật/tắt campaign trên event cụ thể
}