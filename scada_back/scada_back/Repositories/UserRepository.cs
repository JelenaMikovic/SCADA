using scada_back.Database;
using scada_back.Models;

namespace scada_back.Repositories
{
    public class UserRepository
    {
        private DatabaseContext dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User? getByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(user => user.Email == email);
        }
    }
}
