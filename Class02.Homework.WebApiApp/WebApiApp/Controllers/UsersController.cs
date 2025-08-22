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
            try
            {
                return Ok(StaticDb.Username);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving users.");
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetSingleUser(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username cannot be null or empty.");
                }

                string lowerCaseUsername = username.ToLower();
                string foundUser = StaticDb.Username.FirstOrDefault(u => u.ToLower() == lowerCaseUsername);

                if (foundUser != null)
                {
                    return Ok(foundUser);
                }

                return NotFound($"User '{username}' not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving user.");
            }
        }
    }
}