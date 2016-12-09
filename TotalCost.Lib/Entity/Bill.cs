using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.Lib.Entity
{
    /// <summary>
    /// Счет
    /// </summary>
    class Bill
    {
        public int Id { get; set; }
        public BillType Type { get; set; }
        public double Sum { get; set; }
        public string Name { get; set; }
        public virtual List<Payment> Payments { get; set; }

        public enum BillType
        {
            Cash,
            Card
        }
    }
}
