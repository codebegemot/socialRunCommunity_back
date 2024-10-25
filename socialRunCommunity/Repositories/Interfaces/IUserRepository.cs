public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByTelegramIdAsync(string telegramId);
}
