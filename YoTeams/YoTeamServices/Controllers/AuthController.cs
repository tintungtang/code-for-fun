using YoTeamServices.Dtos;
using YoTeamServices.Entities;
using YoTeamServices.Services;

namespace YoTeamServices.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpGet("validate")]
    public IActionResult Validate()
    {
        var user = HttpContext.User;

        Console.WriteLine("validate");

        if (user.Identity is not { IsAuthenticated: true })
        {
            Console.WriteLine("Invalid or expired token.");
            return Unauthorized(new { message = "Invalid or expired token." });
        }

        return Ok(new
        {
            message = "Token is valid.",
            username = user.Identity.Name,
            roles = new List<Role>()
        });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var result = await _authService.RegisterUser(dto);
        if (!result) return BadRequest(new { message = "Username already exists." });

        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var isAuthenticatedUser = await _authService.AuthenticateUser(dto);
    
        if (isAuthenticatedUser == null)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        var token = _authService.GenerateJwtToken(isAuthenticatedUser);

        Response.Cookies.Append("AUTH_JWT", token, new CookieOptions
        {
            HttpOnly = true,  
            Secure = true,    
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new { message = "Login successful." });
    }

}
