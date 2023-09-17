using Microsoft.AspNetCore.Mvc;
using scada_back.Exceptions;
using scada_back.Models;
using scada_back.Repositories;
using scada_back.Services.IServices;

namespace scada_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{email}/{password}")]
        public IActionResult Authenticate(string email, string password)
        {
            try
            {
                var user = this.userService.AuthenticateUser(email, password);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EmailAndPasswordDontMatchException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
