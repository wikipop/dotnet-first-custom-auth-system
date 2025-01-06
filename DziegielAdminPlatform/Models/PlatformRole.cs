using System.ComponentModel.DataAnnotations;

namespace DziegielAdminPlatform.Models;

public class PlatformRole
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}