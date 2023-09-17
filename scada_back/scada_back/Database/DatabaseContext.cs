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

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Bob", LastName = "Majstor", IsAdmin = true, Email = "bob@admin.com", Password = "bob123" },
                new User { Id = 2, Name = "Bob", LastName = "Ross", IsAdmin = false, Email = "bob@client.com", Password = "bob123" }
            );
        }
    }
}
