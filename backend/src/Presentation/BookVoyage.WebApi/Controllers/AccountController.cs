using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Domain.Entities.UserAggegate;
using BookVoyage.Utility.Constants;
using BookVoyage.WebApi.DTOs;
using BookVoyage.WebApi.Services;

namespace BookVoyage.WebApi.Controllers;
/// <summary>
/// Represents the accounts controller
/// </summary>
[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<ApplicationUser> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }
    
    /// <summary>
    /// User authentication
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.V1.Auth.Login)]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
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
    
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.V1.Users.Create)]
    public async Task<ActionResult<LoginResponseDto>> Register(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
        {
            return BadRequest("Username is already taken.");
        }
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        {
            return BadRequest("Email is already used. ");
        }
        var user = new ApplicationUser
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

    /// <summary>
    /// Get the current user with token
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet(ApiEndpoints.V1.Users.Get)]
    public async Task<ActionResult<LoginResponseDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        return CreateUserObject(user);
    }
    
    // Creates a response object for user authentication 
    private LoginResponseDto CreateUserObject(ApplicationUser user)
    {
        return new LoginResponseDto
        {
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }
}