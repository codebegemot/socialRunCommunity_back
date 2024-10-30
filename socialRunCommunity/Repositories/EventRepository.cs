using Microsoft.EntityFrameworkCore;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Event> GetEventAsync(int skip)
    {
        return await _context.Events
            .OrderBy(e => e.Id)
            .Skip(skip)
            .Take(1)
            .FirstOrDefaultAsync();
    }
}