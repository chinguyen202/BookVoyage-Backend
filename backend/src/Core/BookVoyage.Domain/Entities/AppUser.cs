using Microsoft.AspNetCore.Identity;

namespace BookVoyage.Domain.Entities;

public class AppUser: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}