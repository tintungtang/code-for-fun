using YoTeamServices.Dtos;
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
            return Unauthorized(new { message = "Invalid username or password." });

        // Generate JWT Token
        var token = _authService.GenerateJwtToken(isAuthenticatedUser);

        // Set Cookie with HttpOnly and Secure attributes
        Response.Cookies.Append("AUTH_JWT", token, new CookieOptions
        {
            HttpOnly = true,  // Prevent access from JavaScript (XSS protection)
            Secure = true,    // Ensure it's only sent over HTTPS
            SameSite = SameSiteMode.Strict, // Prevent CSRF attacks
            Expires = DateTime.UtcNow.AddDays(7) // Set expiration time
        });

        return Ok(new { message = "Login successful." });
    }
}
