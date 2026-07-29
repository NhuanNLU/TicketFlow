using TicketFlow.Repository.Abstractions;
using TicketFlow.Repository.Enum.Common;
using TicketFlow.Repository.Enum.Idol;

namespace TicketFlow.Repository.Entities;

public class Idol: EntityAuditSoftDeleteBase<Guid>
{
    public required string StageName { get; set; }
    public string? RealName { get; set; }
    public string? Avatar { get; set; }
    public string? Description { get; set; }
    public string? Nationality { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }     // 👈 thêm: ngày sinh
    public GenderEnum? Gender { get; set; }                     // 👈 thêm: giới tính
    public IdolRoleTypeEnum RoleType {get; set;}
    public string? SocialLinks { get; set; }             // 👈 thêm: JSON chứa link MXH
    public string? Genres { get; set; }                  // 👈 thêm: JSON thể loại ["Pop", "Ballad"]
    public ICollection<IdolEvent> IdolEvents { get; set; } =  new List<IdolEvent>();
}