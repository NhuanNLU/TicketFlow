using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class BookingDetail: EntityBase<Guid>
{
    public Booking Booking { get; set; }
    public Guid BookingId { get; set; }
    //Thiếu thông tin ghế
    public decimal Price { get; set; }
}