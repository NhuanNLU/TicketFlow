using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository.Entities;

namespace TicketFlow.Repository;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Idol> Idols { get; set; }
    public DbSet<IdolEvent> IdolEvents { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingDetail> BookingDetails { get; set; }
    public DbSet<BookingCampaign> BookingCampaigns { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<CampaignEvent> CampaignEvents { get; set; }
}