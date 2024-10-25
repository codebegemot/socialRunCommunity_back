using Microsoft.EntityFrameworkCore;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByTelegramIdAsync(string telegramId)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.TelegramId == telegramId);
    }
}
