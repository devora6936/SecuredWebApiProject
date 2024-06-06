using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities;

public partial class UserDTO
{
    public int UserId { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    [MaxLength(50,ErrorMessage ="שם פרטי לא יכול להיות יותר מ50 תווים")]

    public string? FirstName { get; set; }
    [MaxLength(50, ErrorMessage = "שם משפחה לא יכול להיות יותר מ50 תווים")]

    public string? LastName { get; set; }

    public string? Message { get; set; }
    [NotMapped]
    public string Token { get; set; }
}
