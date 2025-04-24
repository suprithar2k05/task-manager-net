using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class UserSignupDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; }
}
