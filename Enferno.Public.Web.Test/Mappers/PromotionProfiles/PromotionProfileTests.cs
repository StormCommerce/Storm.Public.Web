
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Test.Mappers.ProductProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Public.Web.Test.Mappers.PromotionProfiles
{
    [TestClass]
    public class PromotionProfileTests
    {
        [TestInitialize]
        public void Init()
        {
            IoC.RegisterType<ISiteRules, TestSiteRules>();
        }

        [TestMethod, TestCategory("UnitTest")]

        public void MappingIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
