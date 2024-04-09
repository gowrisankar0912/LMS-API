using LibraryManagement.Data;
using LibraryManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryManagementDbContext _dbContext;
        public UserService(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
