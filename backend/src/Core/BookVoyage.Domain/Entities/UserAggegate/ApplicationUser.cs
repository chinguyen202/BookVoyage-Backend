using BookVoyage.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace BookVoyage.Domain.Entities.UserAggegate;

public class ApplicationUser: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserAddress Address { get; set; }
}