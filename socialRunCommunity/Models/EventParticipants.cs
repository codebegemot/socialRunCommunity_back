public class EventParticipant
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User{ get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public DateTime RegistredAt { get; set; } = DateTime.Now;
    public bool IsCanceled { get; set; } = false;
}