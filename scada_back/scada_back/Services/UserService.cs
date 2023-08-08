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
    }
}
