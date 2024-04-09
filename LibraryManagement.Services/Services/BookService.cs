using LibraryManagement.Data;
using LibraryManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryManagementDbContext _dbContext;
        public BookService(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
