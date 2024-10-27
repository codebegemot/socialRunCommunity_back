public class Organizer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<EventOrganizer> EventOrganizers { get; set; }
}
