using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
