using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Entity
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
    }
}
