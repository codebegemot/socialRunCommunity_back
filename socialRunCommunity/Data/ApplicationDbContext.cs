using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events{ get; set; }
    public DbSet<EventParticipant> EventParticipants{ get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<EventOrganizer> EventOrganizers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventParticipant>()
            .HasKey(ep => ep.Id);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.User)
            .WithMany(u => u.EventParticipants)
            .HasForeignKey(ep => ep.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(ep => ep.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.TelegramId)
            .IsUnique();

        modelBuilder.Entity<Event>()
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Event>()
            .Property(e => e.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<EventOrganizer>()
            .HasKey(eo => new { eo.EventId, eo.OrganizerId });

        modelBuilder.Entity<EventOrganizer>()
            .HasOne(eo => eo.Event)
            .WithMany(e => e.EventOrganizers)
            .HasForeignKey(eo => eo.EventId);

        modelBuilder.Entity<EventOrganizer>()
            .HasOne(eo => eo.Organizer)
            .WithMany(o => o.EventOrganizers)
            .HasForeignKey(eo => eo.OrganizerId);

        // Ограничения на длину строк
        modelBuilder.Entity<Event>()
            .Property(e => e.Title)
            .HasMaxLength(30);

        modelBuilder.Entity<Event>()
            .Property(e => e.Location)
            .HasMaxLength(50);
    }
}