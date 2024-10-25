public class User
{
    public int Id { get; set; }
    public string TelegramId { get; set; } = string.Empty;
    public string Username { get; set;} = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<EventParticipant> EventParticipants{ get; set; }
}