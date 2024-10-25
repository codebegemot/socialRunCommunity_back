using Microsoft.EntityFrameworkCore;

public class EventParticipantRepository : Repository<EventParticipant>, IEventParticipantRepository
{
    public EventParticipantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EventParticipant>> GetParticipantsByEventIdAsync(int eventId)
    {
        return await _context.EventParticipants
            .Where(ep => ep.EventId == eventId)
            .ToListAsync();
    }
}
