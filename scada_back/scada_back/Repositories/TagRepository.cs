using scada_back.Database;

namespace scada_back.Repositories
{
    public class TagRepository
    {
        private DatabaseContext dbContext;

        public TagRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
