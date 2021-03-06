﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Group Group { get; set; }
        public Bill Bill { get; set; }

        public override string ToString()
        {
            return $"{Date.DayOfWeek}: {Group} {Sum}";
        }
    }

}
