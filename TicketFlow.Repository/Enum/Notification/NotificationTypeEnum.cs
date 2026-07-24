namespace TicketFlow.Repository.Enum.Notification;

public enum NotificationTypeEnum
{
    BookingConfirm,     // Xác nhận đặt vé
    PaymentSuccess,     // Thanh toán thành công
    PaymentFailed,      // Thanh toán thất bại
    EventReminder,      // Nhắc nhở sự kiện
    CheckinSuccess,     // Check-in thành công
    Campaign,           // Khuyến mãi
    System,             // Thông báo hệ thống
    EventUpdate,        // Cập nhật sự kiện
    Cancellation        // Hủy vé
}
