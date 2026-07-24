namespace TicketFlow.Repository.Enum.Booking;

public enum StatusBookingEnum
{
    Pending,          // Ghế đang hold, chờ thanh toán
    WaitingPayment,   // Đã chuyển sang cổng thanh toán
    Paid,             // Đã thanh toán
    Failed,           // Thanh toán thất bại
    Expired,          // Hết hạn giữ ghế
    Cancelled,        // Bị hủy
    Refunded,         // Đã hoàn tiền
    Completed         // Đã check-in / hoàn tất
}