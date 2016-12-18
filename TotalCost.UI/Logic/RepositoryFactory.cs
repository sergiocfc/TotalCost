using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCost.UI.Logic
{
    class RepositoryFactory
    {
        private static RepositoryFactory _default;

        public static RepositoryFactory Default
        {
            get {
                if (_default == null)
                    _default = new RepositoryFactory();
                return _default; }
        }

        public IDataRepository GetRepository()
        {
            return new Repository();
        }
    }
}
