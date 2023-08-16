using System;
using scada_back.Models;

namespace scada_back.Repositories.IRepositories
{
	public interface IUserRepository<T> where T : User
    {
        Task<T> GetByEmail(string email);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T user);
    }
}

