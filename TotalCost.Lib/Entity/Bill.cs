using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Lib
{
    /// <summary>
    /// Счет
    /// </summary>
    public class Bill
    {
        public int Id { get; set; }
        public BillType Type { get; set; }
        public string Name { get; set; }
        public virtual List<Payment> Payments { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Тип счета
    /// </summary>
    public enum BillType
    {
        Cash,
        Card
    }
}
