using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class BookingDetail: EntityBase<Guid>
{
    public Booking Booking { get; set; }
    public Guid BookingId { get; set; }
    public decimal Price { get; set; }
    public EventSeat EventSeat { get; set; }
    public Guid EventSeatId { get; set; }
}