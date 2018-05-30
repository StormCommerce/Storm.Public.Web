using Enferno.Public.Web.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Public.Web.Test
{
    [TestClass]
    public class TestBootstapper
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            WebProfile.Configure();
        }
    }
}
