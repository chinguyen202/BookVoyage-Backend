using System.Security.Claims;
using BookVoyage.Application.Common;
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
    public async Task<ActionResult<ApiResult<LoginResponseDto>>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return ApiResult<LoginResponseDto>.Failure("The user doesn't exist. Please check your email again!");
        }
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result)
        {
            var responseDto =await  CreateUserObject(user);
            return ApiResult<LoginResponseDto>.Success(responseDto);
        }
        return ApiResult<LoginResponseDto>.Failure("Invalid email or password");
    }
    
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.V1.Users.Create)]
    public async Task<ActionResult<ApiResult<LoginResponseDto>>> Register(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
        {
            return ApiResult<LoginResponseDto>.Failure("Username is already exist.");
        }
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        {
            return ApiResult<LoginResponseDto>.Failure(("Email is already used."));
        }
        var user = new ApplicationUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, SD.Customer);
            var responseDto =await CreateUserObject(user);
            return ApiResult<LoginResponseDto>.Success(responseDto);
        }

        return ApiResult<LoginResponseDto>.Failure(result.Errors.ToString());
    }
    
    // Creates a response object for user authentication 
    private async Task<LoginResponseDto> CreateUserObject(ApplicationUser user)
    {
        var token = await _tokenService.CreateToken(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        return new LoginResponseDto
        {
            UserName = user.UserName,
            Token = token
        };
    }
}