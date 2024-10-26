using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] string telegramId)
    {
        var user = await _authService.AuthenticateUserAsync(telegramId);
        if (user == null)
        {
            return Unauthorized();
        }
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        await _authService.RegisterUserAsync(user);
        return Ok();
    }
}