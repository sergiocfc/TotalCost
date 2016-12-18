using System.Data.Entity;

namespace TotalCost.UI.Lib
{
    public class Context : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public Context() : base("localsql")
        { }
    }
}
