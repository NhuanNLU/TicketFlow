using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Booking: EntityAuditBase<Guid>
{
     public Customer Customer { get; set; }
     public Guid CustomerId { get; set; }
     public Event Event { get; set; }
     public Guid EventId { get; set; }
     public decimal FinalPrice { get; set; }
     public int Status { get; set; }
     public string? Note { get; set; }
     public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
     public ICollection<BookingCampaign>  BookingCampaigns { get; set; } = new List<BookingCampaign>();
}