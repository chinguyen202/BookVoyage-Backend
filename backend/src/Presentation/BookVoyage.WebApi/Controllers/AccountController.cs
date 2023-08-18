using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;
using BookVoyage.WebApi.DTOs;
using BookVoyage.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.WebApi.Controllers;

[AllowAnonymous]
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
    
    // User login 
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
    
    // User register
    [HttpPost(ApiEndpoints.Users.Create)]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
        {
            return BadRequest("Username is already taken.");
        }
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        {
            return BadRequest("Email is already used. ");
        }

        var user = new AppUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.FirstName,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            return new UserDto
            {
                UserName = user.UserName,
                Image = null,
                Token = _tokenService.CreateToken(user)
            };
        }

        return BadRequest(result.Errors);
    }
}