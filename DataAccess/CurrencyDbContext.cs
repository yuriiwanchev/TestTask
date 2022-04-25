using DataAccess.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class CurrencyDbContext : DbContext
    {
        public DbSet<CurrencyDb> CurrencyDbs { get; set; }
        public DbSet<CurrencyDataDb> CurrencyDataDbs { get; set; }

        public CurrencyDbContext()
        {
        }

        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyDb>()
                .HasKey(c => new { c.ParentCode, c.Date });
        }
    }
}