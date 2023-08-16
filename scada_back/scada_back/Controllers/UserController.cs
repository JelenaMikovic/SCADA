using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scada_back.DTOs;
using scada_back.Models;
using scada_back.Services.IServices;

namespace scada_back.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly List<User> _users = new List<User>();

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(DTOs.UserRegDTO user)
        {
            if (!_userService.UserExists(user.Email).Result)
            {
                await _userService.AddUser(user);
                return Ok();
            }
            else
            {
                return Conflict("USER WITH THIS EMAIL ALREADY EXISTS");
            }

        }

    }
}
