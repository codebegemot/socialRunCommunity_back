public class Event 
{
    public int Id { get; set;}
    public string Title { get; set;}
    public string Description { get; set;}
    public DateTime EventDate { get; set;}
    public int MaxParticipants { get; set;}
    public string ImageUrl { get; set;}
    public string Location { get; set;}
    public ICollection<EventOrganizer> EventOrganizers { get; set;}
    public ICollection<EventParticipant> EventParticipants{ get; set;}
}