using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository.Abstractions.Entities;
using TicketFlow.Repository.Entities;

namespace TicketFlow.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Organizer> Organizers => Set<Organizer>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Idol> Idols => Set<Idol>();
    public DbSet<IdolEvent> IdolEvents => Set<IdolEvent>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();
    public DbSet<BookingCampaign> BookingCampaigns => Set<BookingCampaign>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<CampaignEvent> CampaignEvents => Set<CampaignEvent>();
    public DbSet<Zone> Zones => Set<Zone>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<EventSeat> EventSeats => Set<EventSeat>();
    public DbSet<EventZone> EventZones => Set<EventZone>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ========== USER ==========
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Bio).HasMaxLength(2000);

            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasOne(e => e.Customer)
                .WithOne(e => e.User)
                .HasForeignKey<Customer>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Organizer)
                .WithOne(e => e.User)
                .HasForeignKey<Organizer>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        // ========== CUSTOMER ==========
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.UserId).IsUnique();
        });

        // ========== ORGANIZER ==========
        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.Property(e => e.OrganizerName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.OrganizerEmail).IsRequired().HasMaxLength(150);
            entity.Property(e => e.OrganizerPhone).IsRequired().HasMaxLength(20);
        });

        // ========== STAFF ==========
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Avatar).HasMaxLength(500);

            entity.HasOne(e => e.Organizer)
                .WithMany(e => e.Staffs)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== DOCUMENT ==========
        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.Image).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FileType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.RejectionReason).HasMaxLength(1000);
            entity.Property(e => e.VerifiedBy).HasMaxLength(100);

            entity.HasOne(e => e.Organizer)
                .WithMany(e => e.Documents)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== REFRESH TOKEN ==========
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ReplacedByToken).HasMaxLength(500);

            entity.HasOne(e => e.User)
                .WithMany(e => e.RefreshTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Token);
        });

        // ========== AUDIT LOG ==========
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.Property(e => e.Action).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EntityName).IsRequired().HasMaxLength(100);

            entity.HasOne(e => e.User)
                .WithMany(e => e.AuditLogs)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== NOTIFICATION ==========
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(2000);

            entity.HasOne(e => e.User)
                .WithMany(e => e.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Ignore(e => e.CreatedDate);
        });

        // ========== REPORT ==========
        modelBuilder.Entity<Report>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);

            entity.HasOne(e => e.User)
                .WithMany(e => e.Reports)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== REVIEW ==========
        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.Content).IsRequired().HasMaxLength(2000);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Organizer)
                .WithMany()
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // ========== VENUE ==========
        modelBuilder.Entity<Venue>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            entity.Property(e => e.MapUrl).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(2000);
        });

        // ========== EVENT ==========
        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.CoverImageUrl).HasMaxLength(500);

            entity.HasOne(e => e.Organizer)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Venue)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== IDOL ==========
        modelBuilder.Entity<Idol>(entity =>
        {
            entity.Property(e => e.StageName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.RealName).HasMaxLength(200);
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.SocialLinks).HasMaxLength(1000);
            entity.Property(e => e.Genres).HasMaxLength(500);
        });

        // ========== IDOL EVENT ==========
        modelBuilder.Entity<IdolEvent>(entity =>
        {
            entity.HasOne(e => e.Event)
                .WithMany(e => e.IdolEvents)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Idol)
                .WithMany(e => e.IdolEvents)
                .HasForeignKey(e => e.IdolId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== BOOKING ==========
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(e => e.FinalPrice).HasPrecision(18, 2);
            entity.Property(e => e.Note).HasMaxLength(1000);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== BOOKING DETAIL ==========
        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.Property(e => e.Price).HasPrecision(18, 2);

            entity.HasOne(e => e.Booking)
                .WithMany(e => e.BookingDetails)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.EventSeat)
                .WithMany()
                .HasForeignKey(e => e.EventSeatId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== BOOKING CAMPAIGN ==========
        modelBuilder.Entity<BookingCampaign>(entity =>
        {
            entity.HasOne(e => e.Booking)
                .WithMany(e => e.BookingCampaigns)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Campaign)
                .WithMany(e => e.BookingCampaigns)
                .HasForeignKey(e => e.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== CAMPAIGN ==========
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.DiscountValue).HasPrecision(18, 2);
            entity.Property(e => e.MaxDiscount).HasPrecision(18, 2);
            entity.Property(e => e.MinOrderAmount).HasPrecision(18, 2);

            entity.HasOne(e => e.Organizer)
                .WithMany()
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.Code).IsUnique();
        });

        // ========== CAMPAIGN EVENT ==========
        modelBuilder.Entity<CampaignEvent>(entity =>
        {
            entity.Property(e => e.DiscountOverride).HasPrecision(18, 2);

            entity.HasOne(e => e.Campaign)
                .WithMany()
                .HasForeignKey(e => e.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== ZONE ==========
        modelBuilder.Entity<Zone>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(e => e.Venue)
                .WithMany(e => e.Zones)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== SEAT ==========
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.Property(e => e.Row).IsRequired().HasMaxLength(10);

            entity.HasOne(e => e.Zone)
                .WithMany(e => e.Seats)
                .HasForeignKey(e => e.ZoneId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== EVENT SEAT ==========
        modelBuilder.Entity<EventSeat>(entity =>
        {
            entity.HasOne(e => e.Event)
                .WithMany(e => e.EventSeats)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Seat)
                .WithMany()
                .HasForeignKey(e => e.SeatId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ========== EVENT ZONE ==========
        modelBuilder.Entity<EventZone>(entity =>
        {
            entity.Property(e => e.Price).HasPrecision(18, 2);

            entity.HasOne(e => e.Event)
                .WithMany(e => e.EventZones)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Zone)
                .WithMany()
                .HasForeignKey(e => e.ZoneId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
