
using System;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Builders;
using Enferno.Public.Web.Mappers;
using Enferno.Public.Web.Mappers.ProductProfiles;
using Enferno.Public.Web.Repositories;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ParametricValue = Enferno.StormApiClient.Products.ParametricValue;

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
            Mapper.AddProfile<ManufacturerToManufacturerModelProfile>();
        }

        [TestMethod]
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
