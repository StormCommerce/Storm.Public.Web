

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Public.Web.Test
{
    [TestClass]
    public class PriceCalulatorTests
    {       
        [TestMethod]
        [Description("Calculate null price returns zero")]
        public void PriceTest03()
        {

            decimal? price = null;
            const decimal vatRate = 1.25M;

            var result = PriceCalulator.Price(price, vatRate);

            Assert.AreEqual(0, result);

        }
    }
}
