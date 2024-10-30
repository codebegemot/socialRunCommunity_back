using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("event")]
    public async Task<IActionResult> GetEvent([FromQuery] int pageNumber)
    {
        if (pageNumber < 1)
        {
            return BadRequest("Page number must be greater than 0.");
        }

        var eventItem = await _eventService.GetEventAsync(pageNumber);
        if (eventItem == null)
        {
            return NotFound("No event found.");
        }

        return Ok(eventItem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventItem = await _eventService.GetEventByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound("Event not found.");
        }

        return Ok(eventItem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] Event eventItem)
    {
        await _eventService.CreateEventAsync(eventItem);
        return CreatedAtAction(nameof(GetEvent), new {id = eventItem.Id}, eventItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event eventItem)
    {
        if(id != eventItem.Id){
            return BadRequest();
        }

        await _eventService.UpdateEventAsync(eventItem);
        return NoContent();
    }

    [HttpPost("{id}/register")]
    public async Task<IActionResult> RegisterUserToEvent(int id, [FromBody] int userId)
    {
        await _eventService.RegisterUserToEventAsync(id, userId);
        return Ok();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelUserRegistration(int id, [FromBody] int userId)
    {
        await _eventService.CancelUserRegistrationAsync(id, userId);
        return Ok();
    }
}