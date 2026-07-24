using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Repository.Entities;

public class RefreshToken: EntityBase<Guid>
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }        // 👈 hạn của token
    public bool IsRevoked { get; set; }                  // 👈 đã thu hồi chưa
    public bool IsUsed { get; set; }                     // 👈 đã dùng chưa (cho rotation)
    public DateTimeOffset CreatedAt { get; set; }        // 👈 thời gian tạo
    public DateTimeOffset? RevokedAt { get; set; }       // 👈 thời gian thu hồi
    public string? ReplacedByToken { get; set; }         // 👈 token thay thế (rotation)
}