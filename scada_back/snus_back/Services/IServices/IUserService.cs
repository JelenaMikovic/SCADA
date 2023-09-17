using scada_back.Models;

namespace scada_back.Services.IServices
{
    public interface IUserService
    {
        public User AuthenticateUser(string email, string password);
    }
}
