namespace TicketFlow.Repository.Enum.Report;

public enum ReportTypeEnum
{
    Spam,//Nội dung spam, quảng cáo  
    Harassment,// Quấy rối, đe dọa
    Inappropriate,// Nội dung không phù hợp 
    WrongInfo,// Thông tin sai lệch (ngày giờ, địa điểm, giá vé)
    Duplicate,//Sự kiện trùng lặp                               │
    CopyrightViolation,//Vi phạm bản quyền 
    Other
}