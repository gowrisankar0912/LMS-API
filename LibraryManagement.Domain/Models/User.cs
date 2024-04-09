using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Models
{
    public class User
    {
        [Key] public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
    }
}
