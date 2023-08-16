using scada_back.Models;

namespace scada_back.Services.IServices
{
    public interface IUserService
    {
        Task AddUser(DTOs.UserRegDTO user);
        Task<bool> UserExists(string email);
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
    }
}
