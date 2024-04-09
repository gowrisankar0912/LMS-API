using LibraryManagement.Data;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LibraryManagementDbContext _dbContext;
        public UserController(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsers() 
        {
            try
            {
                List<User> users = _dbContext.Users.ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            try
            {
                User user = _dbContext.Users.Find(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                LoginInfo log = new LoginInfo { Username = user.Username, Password = user.Password };
                _dbContext.LoginInfos.Add(log);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest("User ID mismatch");
                User updateUser = _dbContext.Users.Find(id);
                if (updateUser == null)
                    return NotFound("User not found");
                LoginInfo updateLog = _dbContext.LoginInfos.FirstOrDefault(l => l.Username.Equals(user.Username));
                if (updateLog == null)
                    return NotFound("LoginInfo not found");
                _dbContext.Entry(updateUser).CurrentValues.SetValues(user);
                updateLog.Username = user.Username;
                updateLog.Password = user.Password;
                _dbContext.SaveChanges();
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                    return NotFound();

                LoginInfo loginInfo = _dbContext.LoginInfos.FirstOrDefault(l => l.Username.Equals(user.Username));
                if (loginInfo != null)
                    _dbContext.LoginInfos.Remove(loginInfo);

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
