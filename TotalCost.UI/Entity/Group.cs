using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Entity
{
    /// <summary>
    /// Группа платежей.
    /// </summary>
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public GroupType Type { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public double Limit { get; set; }
    }

    /// <summary>
    /// Тип группы.
    /// </summary>
    public enum GroupType
    {
        /// <summary>
        /// Доход.
        /// </summary>
        Income,
        /// <summary>
        /// Расход.
        /// </summary>
        Consumption
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
