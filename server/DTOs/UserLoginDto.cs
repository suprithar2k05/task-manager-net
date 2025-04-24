using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class UserLoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string? PasswordHash { get; set; }
}
