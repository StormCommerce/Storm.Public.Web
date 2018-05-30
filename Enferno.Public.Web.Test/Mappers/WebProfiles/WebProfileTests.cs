
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Test.Mappers.ProductProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enferno.Public.Web.Test.Mappers.WebProfiles
{
    [TestClass]
    public class WebProfileTests
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
