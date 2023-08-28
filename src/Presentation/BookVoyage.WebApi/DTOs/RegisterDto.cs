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
    public string Password { get; set; }
    [Required]
    public string Username   { get; set; }
}