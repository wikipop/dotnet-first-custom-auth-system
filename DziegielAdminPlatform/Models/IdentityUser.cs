﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DziegielAdminPlatform.Models;

public class IdentityUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}