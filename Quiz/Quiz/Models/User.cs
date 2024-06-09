using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Role { get; set; }
        public virtual Role Roles { get; set; } = null;
    }
}
