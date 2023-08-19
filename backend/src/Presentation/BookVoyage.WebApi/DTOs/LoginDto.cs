namespace BookVoyage.WebApi.DTOs;

/// <summary>
/// Data transfer object (DTO) class used for user authentication.
/// </summary>
public class LoginDto
{
     public string Email { get; set; }
     public string Password { get; set; }
}