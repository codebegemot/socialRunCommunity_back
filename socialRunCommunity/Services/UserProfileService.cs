public class UserProfileService
{
    private readonly IUserRepository _userRepository;
    private readonly IEventParticipantRepository _eventParticipantRepository;

    public UserProfileService(IUserRepository userRepository, IEventParticipantRepository eventParticipantRepository)
    {
        _userRepository = userRepository;
        _eventParticipantRepository = eventParticipantRepository;
    }

    public async Task<User?> GetUserProfileAsync(int userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<IEnumerable<EventParticipant>> GetUserEventHistoryAsync(int userId)
    {
        return await _eventParticipantRepository.GetAllAsync();
    }

    public async Task UpdateUserProfileAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }
}