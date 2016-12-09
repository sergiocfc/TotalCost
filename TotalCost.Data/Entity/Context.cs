using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.Data.Entity
{
    class Context : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Limit> Limits { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public Context() : base("totalcost")
        {

        }
    }
}
