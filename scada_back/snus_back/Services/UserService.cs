using scada_back.Exceptions;
using scada_back.Models;
using scada_back.Repositories;
using scada_back.Services.IServices;

namespace scada_back.Services
{
    public class UserService: IUserService
    {
        private UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = this.userRepository.getByEmail(email);
            if (user == null) throw new UserNotFoundException();
            if (user.Password == password) return user; 
            else throw new EmailAndPasswordDontMatchException();
        }
    }
}
