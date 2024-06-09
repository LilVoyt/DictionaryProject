using Microsoft.EntityFrameworkCore;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data
{
    internal class QuizContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Role> Roles { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-U5SNHKPD\\SQLEXPRESS;Database=Quiz;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
