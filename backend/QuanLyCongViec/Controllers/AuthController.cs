﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyCongViec.Models.Core;
using QuanLyCongViec.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }

            return Unauthorized(new { message = "Invalid username or password" });
        }

        return BadRequest(ModelState);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new UserMeDTO(user, roles));
    }

    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDTO updateUserDTO)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        user.Information = updateUserDTO.Information ?? user.Information;
        user.ProfilePictureLink = updateUserDTO.ProfilePictureLink ?? user.ProfilePictureLink;
        user.SocialLinks = updateUserDTO.SocialLinks ?? user.SocialLinks;
        user.Email = updateUserDTO.Email ?? user.Email;
        user.PhoneNumber = updateUserDTO.PhoneNumber ?? user.PhoneNumber;

        // Cập nhật người dùng trong cơ sở dữ liệu
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(new { message = "Error updating user information", errors = result.Errors });
        }

        return Ok(new { message = "User information updated successfully" });
    }


    [HttpPut("me/password")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO updatePasswordDTO)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, updatePasswordDTO.CurrentPassword);
        if (!isCurrentPasswordValid)
        {
            return BadRequest(new { message = "Current password is incorrect" });
        }

        var result = await _userManager.ChangePasswordAsync(user, updatePasswordDTO.CurrentPassword, updatePasswordDTO.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(new { message = "Error changing password", errors = result.Errors });
        }

        return Ok(new { message = "Password updated successfully" });
    }

    [HttpDelete("me")]
    [Authorize]
    public async Task<IActionResult> DeleteMe()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return StatusCode(500, new { message = "Failed to delete the user" });
        }

        return Ok(new { message = "Delete your account successfully!"});
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");
        var expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "");
        var Issuer = _configuration["Jwt:Issuer"];
        var Audience = _configuration["Jwt:Audience"];
        var roles = _userManager.GetRolesAsync(user).Result;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? "UserName")
        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
