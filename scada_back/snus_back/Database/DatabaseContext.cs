using Microsoft.EntityFrameworkCore;
using scada_back.Models;

namespace scada_back.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("scada");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Device> Devices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Bob", LastName = "Majstor", IsAdmin = true, Email = "bob@admin.com", Password = "bob123" },
                new User { Id = 2, Name = "Bob", LastName = "Ross", IsAdmin = false, Email = "bob@client.com", Password = "bob123" }
            );

        }
    }
}
