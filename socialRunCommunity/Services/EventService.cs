public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IEventParticipantRepository _eventParticipantRepository;

    public EventService(IEventRepository eventRepository, IEventParticipantRepository eventParticipantRepository)
    {
        _eventRepository = eventRepository;
        _eventParticipantRepository = eventParticipantRepository;
    }
    
    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int eventId)
    {
        return await _eventRepository.GetByIdAsync(eventId);
    }

    public async Task CreateEventAsync(Event eventItem)
    {
        await _eventRepository.AddAsync(eventItem);
    }

    public async Task UpdateEventAsync(Event eventItem)
    {
        await _eventRepository.UpdateAsync(eventItem);
    }

    public async Task DeleteEventAsync(int eventId)
    {
        await _eventRepository.DeleteAsync(eventId);
    }

    public async Task RegisterUserToEventAsync(int userId, int eventId)
    {
        var participant = new EventParticipant
        {
            UserId = userId,
            EventId = eventId,
            RegistredAt = DateTime.Now
        };
        await _eventParticipantRepository.AddAsync(participant);
    }

    public async Task CancelUserRegistrationAsync(int userId, int eventId)
    {
        var participant = await _eventParticipantRepository.GetParticipantsByEventIdAsync(eventId);
        var userRegistration = participant.FirstOrDefault(p => p.UserId == userId);
        
        if(userRegistration != null){
            userRegistration.IsCanceled = true;
            await _eventParticipantRepository.UpdateAsync(userRegistration);
        }
    }
}