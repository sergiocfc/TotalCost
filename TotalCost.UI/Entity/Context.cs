using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Entity
{
    class Context : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Limit> Limits { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=totalcost.sqlite");
        }
    }
}
