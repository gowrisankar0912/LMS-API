using LibraryManagement.Data;
using LibraryManagement.Domain.Models;
using LibraryManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly LibraryManagementDbContext _dbContext;
        public LoginService(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Login(LoginInfo loginInfo)
        {
            try
            {
                LoginInfo? log = _dbContext.LoginInfos.FirstOrDefault(l => l.Username.Equals(loginInfo.Username) && l.Password.Equals(loginInfo.Password));
                if (log == null) { Console.WriteLine("No User found"); return false; }
                Console.WriteLine($"{log.Username} and {log.Password} found");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return false;
            }
        }
    }
}
