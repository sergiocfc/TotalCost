
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TotalCost.UI.Logic;
using TotalCost.UI.Entity;

namespace TotalCost.Test
{
    [TestClass]
    public class RepositoryTest
    {
        Context _context;

        public RepositoryTest()
        {
            _context = new Context();
        }

        [TestMethod]
        public void Can_add_bill()
        {
            using (var repo = new Repository())
            {

            }
        }
    }
}
