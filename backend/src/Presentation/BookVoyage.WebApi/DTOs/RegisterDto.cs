using System.ComponentModel.DataAnnotations;

namespace BookVoyage.WebApi.DTOs;

/// <summary>
/// Data transfer object (DTO) class used for user registration.
/// </summary>
public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z].{4,8})$", 
        ErrorMessage = "Password must include at least one digit, one lowercase letter, one uppercase letter, and be 4 to 8 characters long.")]
    public string Password { get; set; }
    [Required]
    public string Username   { get; set; }
}