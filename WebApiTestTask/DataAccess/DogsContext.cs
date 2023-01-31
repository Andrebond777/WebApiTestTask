using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestTask.Entities;

namespace WebApiTestTask.DataAccess
{
    public class DogsContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DogsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DogsDb;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
