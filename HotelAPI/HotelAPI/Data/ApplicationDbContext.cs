using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    Id = 1,
                    Name = "Test",
                    City = "Medellin",
                    Detail = "Detail Test",
                    Capacity = 100,
                    Price = 10000.5,
                    ImageURL = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                },
                new Hotel()
                {
                    Id = 2,
                    Name = "Test 2",
                    City = "Bogotá",
                    Detail = "Detail Test",
                    Capacity = 200,
                    Price = 20000.5,
                    ImageURL = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                }
            );

        }
    }
}
