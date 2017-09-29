using System;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Mappers;
using Enferno.Public.Web.Mappers.ProductProfiles;
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

            Mapper.Initialize(conf => conf.AddProfile<WebProfile>());
        }

        [TestMethod]
        public void MappingIsValid()
    {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
