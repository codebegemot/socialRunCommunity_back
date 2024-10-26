using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly UserProfileService _userProfileService;
    public UserProfileController(UserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfile(int id)
    {
        var user = await _userProfileService.GetUserProfileAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetUserEventHistory(int id)
    {
        var history = await _userProfileService.GetUserEventHistoryAsync(id);
        return Ok(history);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        await _userProfileService.UpdateUserProfileAsync(user);
        return NoContent();
    }
}