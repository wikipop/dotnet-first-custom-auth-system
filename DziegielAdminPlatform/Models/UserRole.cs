using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DziegielAdminPlatform.Models;

public class UserRole
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public PlatformRole Role { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public PlatformUser User { get; set; } = null!;
}