using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalCost.UI.Entity;

namespace TotalCost.UI.ViewModels
{
    class StatByGroupViewModel
    {
        public Group Group { get; set; }
        public double Min { get; set; }
        public double Average { get; set; }
        public double Max { get; set; }
    }
}
