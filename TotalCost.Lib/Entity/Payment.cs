using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Lib
{
    /// <summary>
    /// Платеж.
    /// </summary>
    public class Payment
    { 
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Sum { get; set; }
        public string Note { get; set; }
        public virtual Group Group { get; set; }
        public virtual Bill Bill { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1} {2}{3}",
                Date.DayOfWeek,
                Group, 
                (Group.Type == GroupType.Income) ? "+" : "-",
                Sum);
        }
    }

}
