using System.ComponentModel.DataAnnotations;

namespace BookVoyage.WebApi.DTOs;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]

    public string LastName { get; set; }
    [Required]

    public string Email { get; set; }
    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z].{4,8})$", ErrorMessage = "Password must be complex")]

    public string Password { get; set; }
    [Required]

    public string Username   { get; set; }
}