public interface IEventRepository : IRepository<Event>
{
    Task<Event> GetEventAsync(int skip);
}
