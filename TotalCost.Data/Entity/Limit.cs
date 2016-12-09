using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.Data.Entity
{
    /// <summary>
    /// Ограничение по расходам на группу
    /// </summary>
    class Limit
    {
        public int Id { get; set; }
        public virtual Group Group { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Sum { get; set; }
        public double SumExceed { get; set; } 
    }
}
