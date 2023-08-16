using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scada_back.Models;
using System.Data;
using System.Security.Claims;
using scada_back.Repositories;
using scada_back.Services.IServices;
using scada_back.DTOs;
using System.Text;
using scada_back.Repositories.IRepositories;

namespace scada_back.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository<User> _userRepository;

        public UserService(IUserRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task AddUser(DTOs.UserRegDTO userDTO)
        {
            User user = new User();
            user.Name = userDTO.Name;
            user.LastName = userDTO.Surname;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            await _userRepository.Add(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await _userRepository.GetByEmail(email);
            return user;
        }

        public async Task<User> GetById(int id)
        {
            User user = await _userRepository.GetById(id);
            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            User user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
