using Microsoft.EntityFrameworkCore;

namespace resevation_be.models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}