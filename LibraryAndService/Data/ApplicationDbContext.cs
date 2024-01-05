using LibraryAndService.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAndService.Data
{
    //Kanske byta namn till HotelDBContext
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Room> Room { get; set; }

        public ApplicationDbContext()
        {
            // en tom konstruktor behövs för att skapa migrations
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=HotelDB;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }
    }
}
