namespace BookVoyage.WebApi.DTOs;

/// <summary>
/// Data transfer object (DTO) class used for authentication response if success.
/// </summary>
public class LoginResponseDto
{
    public string UserName { get; set; }
    public string Token { get; set; }
}