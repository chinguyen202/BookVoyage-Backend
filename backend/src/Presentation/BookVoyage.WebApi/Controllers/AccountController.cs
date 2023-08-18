using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;
using BookVoyage.WebApi.DTOs;
using BookVoyage.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, TokenService tokenService )
    {
        _userManager = userManager;
        _tokenService = tokenService;
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
                Token = _tokenService.CreateToken(user),
                Image = null
            };
        }

        return Unauthorized();
    }
}