
using FlashpointStudioManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashpointStudioManager.Data
{
    public class StudioDbContext : DbContext
    {
        public StudioDbContext(
            DbContextOptions<StudioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
