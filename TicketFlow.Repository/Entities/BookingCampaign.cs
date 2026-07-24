using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class BookingCampaign: EntityBase<Guid>
{
    public Booking Booking { get; set; }
    public Guid BookingId { get; set; }
    public Campaign Campaign { get; set; }
    public Guid CampaignId { get; set; }
}