using FullStackWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.DbContexts
{
    public class MainSqlServerDbContext : DbContext
    {
        public MainSqlServerDbContext(DbContextOptions<MainSqlServerDbContext> options) : base(options)
        {

        }

        public DbSet<Aanbod> Aanbod { get; set; }
        public DbSet<Makelaar> Makelaar { get; set; }

    }
}
