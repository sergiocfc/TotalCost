using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Entity
{
    /// <summary>
    /// Ограничение по расходам на группу.
    /// </summary>
    public class Limit
    {
        public int Id { get; set; }
        public virtual Group Group { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Sum { get; set; }
        public double SumExceed { get; set; } 
        public TimeIntervalType TimeType { get; set; }
    }

    /// <summary>
    /// Тип временного интервала.
    /// </summary>
    public enum TimeIntervalType
    {
        Day,
        Week,
        Month,
        Year,
        Custom
    }
}
