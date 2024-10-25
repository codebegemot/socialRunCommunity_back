public interface IEventParticipantRepository : IRepository<EventParticipant>
{
    Task<IEnumerable<EventParticipant>> GetParticipantsByEventIdAsync(int eventId);
}
