using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyCongViec.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AuthController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        return BadRequest(ModelState);
    }
}

public class RegisterModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
