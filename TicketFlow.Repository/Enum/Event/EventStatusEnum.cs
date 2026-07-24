namespace TicketFlow.Repository.Enum.Event;

public enum EventStatusEnum
{
    Draft,              // Nháp
    PendingApproval,    // Chờ Admin duyệt
    Published,          // Đã xuất bản
    SoldOut,            // Hết vé
    Completed,          // Đã kết thúc
    Cancelled           // Đã hủy
}
