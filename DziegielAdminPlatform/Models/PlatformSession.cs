using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DziegielAdminPlatform.Models;

public class PlatformSession
{
    [Key] public Guid SessionId { get; set; }

    [ForeignKey("UserId")] [Required] public PlatformUser User { get; set; }

    public Guid UserId { get; set; }
    public DateTime LastAccessedAt { get; set; }
    public bool IsExpired => LastAccessedAt.AddMinutes(30) < DateTime.UtcNow;
}