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

        // Map Event to EventDTO
        var eventDTOs = events.Select(e => new EventDTO
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            EventDate = e.EventDate,
            MaxParticipants = e.MaxParticipants,
            ImageUrl = e.ImageUrl,
            Location = e.Location
        }).ToList();

        return Ok(eventDTOs);
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

        // Map Event to EventDTO and return as a single-element array
        var eventDTO = new EventDTO
        {
            Id = eventItem.Id,
            Title = eventItem.Title,
            Description = eventItem.Description,
            EventDate = eventItem.EventDate,
            MaxParticipants = eventItem.MaxParticipants,
            ImageUrl = eventItem.ImageUrl,
            Location = eventItem.Location
        };

        return Ok(new List<EventDTO> { eventDTO });
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

    [HttpDelete]
    public async Task<IActionResult> DeleteEvent(int id){
        await _eventService.DeleteEventAsync(id);
        return Ok();
    }
}