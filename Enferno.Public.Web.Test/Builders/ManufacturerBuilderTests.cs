
using System;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Builders;
using Enferno.StormApiClient.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Enferno.Public.Web.Test.Builders
{
    [TestClass]
    public class ManufacturerBuilderTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var rules = MockRepository.GenerateMock<ISiteRules>();
            IoC.RegisterInstance(typeof (ISiteRules), rules);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildManufacturerModelTest01()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();

            var manufacturer = new Manufacturer
            {
                Id = 1, Key = Guid.Empty, LogoKey = Guid.NewGuid(), LogoPath = "Path", Name = "Mfr", UniqueName = "mfr"
            };

            // Act
            var builder = new ManufacturerBuilder(rules);
            var result = builder.BuildManufacturerModel(manufacturer);

            // Assert
            Assert.IsNotNull(result.ImageUrl);
        }
    }
}
