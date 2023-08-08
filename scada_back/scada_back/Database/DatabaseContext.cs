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

    }
}
