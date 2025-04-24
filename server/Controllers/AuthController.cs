using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Helpers;
using server.Models;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] UserSignupDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var user = await _authService.RegisterAsync(dto);
            HttpContext.Session.SetString("UserId", user.Id);
            return Ok(new { message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Not logged in" });

        return Ok(new { userId });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetString("UserId"); // Or from JWT if using token

        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Not logged in" });

        var user = await _authService.GetProfileAsync(userId);

        if (user == null)
            return NotFound(new { message = "User not found" });


        return Ok(new ApiResponse("Profile fetched successfully", user));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        try
        {
            var user = await _authService.AuthenticateAsync(dto);
            if (user == null) return Unauthorized(new ApiResponse("Invalid credentials"));

            HttpContext.Session.SetString("UserId", user.Id);
            return Ok(new ApiResponse("Login successful", new { userId = user.Id }));
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new ApiResponse("Logged out"));
    }
}
