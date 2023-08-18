using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;
using BookVoyage.WebApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized();
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result)
        {
            return new UserDto()
            {
                UserName = user.UserName,
                Token = "this is token",
                Image = null
            };
        }

        return Unauthorized();
    }
}