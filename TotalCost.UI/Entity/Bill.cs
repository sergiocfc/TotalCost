﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Entity
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
