using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyCongViec.Models.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("grant/{uuid}/{roleName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Grant(string uuid, string roleName)
    {
        if (!Guid.TryParse(uuid, out var userGuid))
        {
            return BadRequest(new { message = "Invalid UUID format" });
        }
        
        var user = await _userManager.FindByIdAsync(uuid);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            return BadRequest(new { message = "Role not found" });
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Any())
        {
            return BadRequest(new { message = "User already has role" });
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            return StatusCode(500, new { message = "Failed to grant role" });
        }

        return Ok(new { message = "Role granted successfully" });
    }

    [HttpGet("roleMembership/{roleName}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<User>>> GetRoleList(string roleName)
    {
        if (roleName == "Admin") return BadRequest(new { message = "Admin information can't be retrieved" });
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return BadRequest(new { message = $"'{roleName}' isn't a valid role name" });
        }
        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        return usersInRole.ToList();
    }
}
