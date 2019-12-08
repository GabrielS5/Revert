using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<Keyword> Keywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().HasMany(x => x.Keywords).WithOne(x => x.Record).HasForeignKey(x => x.RecordId);
        }
    }
}