using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.Data.Entity
{
    /// <summary>
    /// Группа платежей
    /// </summary>
    class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public List<Limit> Limits { get; set; }

        public enum GroupType
        {
            Income,
            Consumption
        }
    }
}
