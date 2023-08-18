using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;
using BookVoyage.WebApi.DTOs;
using BookVoyage.WebApi.Services;

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
    
    // User login 
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized();
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result)
        {
            return CreateUserObject(user);
        }
        return Unauthorized();
    }
    
    // User register
    [AllowAnonymous]
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
            return CreateUserObject(user);
        }

        return BadRequest(result.Errors);
    }

    // Get an user from token
    [Authorize]
    [HttpGet(ApiEndpoints.Users.Get)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        return CreateUserObject(user);
    }
    
    private UserDto CreateUserObject(AppUser user)
    {
        return new UserDto
        {
            UserName = user.UserName,
            Image = null,
            Token = _tokenService.CreateToken(user)
        };
    }
}