using Microsoft.EntityFrameworkCore;
using SimpleWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebMVC.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {
            
        }
        public DbSet<Division> Divisions { get; set; }
    }
}
