using Microsoft.EntityFrameworkCore;

namespace scada_back.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }
    }
}
