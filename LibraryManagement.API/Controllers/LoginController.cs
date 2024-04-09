using LibraryManagement.Data;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LibraryManagementDbContext _dbContext;
        public LoginController(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetLogin()
        {
            try
            {
                List<LoginInfo> loginInfos = _dbContext.LoginInfos.ToList();
                return Ok(loginInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo loginInfo)
        {
            try
            {
                var log = _dbContext.LoginInfos.FirstOrDefault(l => l.Username.Equals(loginInfo.Username) && l.Password.Equals(loginInfo.Password));
                if (log == null)
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }
                return Ok(new { message = "Login successful", user = log.Username });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred during login: {ex.Message}" });
            }
        }
    }
}
