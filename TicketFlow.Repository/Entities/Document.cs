using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class Document: EntityAuditSoftDeleteBase<Guid>
{
    public Organizer Organizer { get; set; }
    public Guid OrganizerId { get; set; }      // Khóa ngoại liên kết đến Organizer — mỗi giấy tờ thuộc về một Organizer

    public required string Image { get; set; }
    public required string FileType { get; set; }       // Định dạng file (VD: "pdf", "jpg", "png")
    public long FileSize { get; set; }                  // Dung lượng file (bytes) — để kiểm soát kích thước upload

    public int DocumentType { get; set; }               // Loại giấy tờ (BusinessLicense, TaxCertificate, IdentityCard, EventPermit,...)

    public bool IsVerified { get; set; }                // Admin đã xác thực giấy tờ này chưa?
    public DateTimeOffset? VerifiedAt { get; set; }     // Thời điểm admin xác thực
    public string? VerifiedBy { get; set; }             // Admin nào đã xác thực
    public string? RejectionReason { get; set; }        // Lý do từ chối nếu giấy tờ không hợp lệ

    public DateTimeOffset? ExpiryDate { get; set; }     // Ngày hết hạn của giấy tờ (VD: GPKD có thời hạn)
}