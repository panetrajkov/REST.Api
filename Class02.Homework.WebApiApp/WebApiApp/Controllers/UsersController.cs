using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(StaticDb.Username);
        }

        [HttpGet("{username}")]
        public IActionResult GetSingleUser(string username)
        {
            if (StaticDb.Username.Contains(username))
            {
                return Ok(username);
            }
            else
            {
                return NotFound($"User '{username}' not found.");
            }
        }
    }
}