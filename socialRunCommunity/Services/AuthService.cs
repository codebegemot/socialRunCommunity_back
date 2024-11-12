namespace socialRunCommunity.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<User?> AuthenticateUserAsync(string telegramId)
    {
        return await _userRepository.GetUserByTelegramIdAsync(telegramId);
    }

    public async Task RegisterUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }
}