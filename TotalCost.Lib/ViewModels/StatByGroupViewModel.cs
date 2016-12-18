using TotalCost.UI.Lib;

namespace TotalCost.Lib.ViewModels
{
    public class StatByGroupViewModel
    {
        public Group Group { get; set; }
        public double Min { get; set; }
        public double Average { get; set; }
        public double Max { get; set; }
    }
}
