using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using scada_back.Database;
using scada_back.Models;
using scada_back.Repositories.IRepositories;

namespace scada_back.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        private readonly DbSet<T> _users;
        private readonly DatabaseContext dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            dbContext = dbContext;
            _users = dbContext.Set<T>();
        }

        public async Task Add(T user)
        {
            await _users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _users.ToListAsync();
        }

        public User? getByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public async Task<T> GetByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<T> GetById(int id)
        {
            return await _users.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
