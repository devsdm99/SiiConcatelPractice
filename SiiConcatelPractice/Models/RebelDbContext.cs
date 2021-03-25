using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiiConcatelPractice.Models
{
    public class RebelDbContext : DbContext
    {
        public DbSet<Rebel> Rebels { get; set; }
        public RebelDbContext()
        {

        }
        public RebelDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Rebel>().HasData(new Rebel()
                {
                    Id = 1,
                    Name = "Rebel 1",
                    Planet = "Mars",
                    Date = DateTime.Now
                });

            }catch(Exception ex)
            {
                Console.WriteLine("Test Model cannot be created");
            }
        }

       
    }
}
