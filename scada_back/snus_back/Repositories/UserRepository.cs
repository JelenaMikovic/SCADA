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
            var users = dbContext.Users.ToList();
            Console.WriteLine($"Received request for email: {users[0].Email}");
            return dbContext.Users.FirstOrDefault(user => user.Email == email);
        }
    }
}
