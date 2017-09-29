using System;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Builders;
using Enferno.Public.Web.Mappers.ProductProfiles;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ParametricValue = Enferno.StormApiClient.Products.ParametricValue;

namespace Enferno.Public.Web.Test.Builders
{
    [TestClass]
    public class ProductBuilderTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var rules = MockRepository.GenerateMock<ISiteRules>();
            IoC.RegisterInstance(typeof (ISiteRules), rules);
            Mapper.AddProfile<ProductProfile>();
        }

        [TestMethod]
        public void BuildProductModelTest01()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            // Act
            var productBuilder = new ProductBuilder(dictionary, rules);
            var result = productBuilder.BuildProductModel(null);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void BuildProductModelTest02()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var product = new Product
            {
                Id = 1,
                Name = "MockProduct",
                Manufacturer = new ProductManufacturer{Id = 1, Name = "MockManufacturer" },
                CategoryId = null,
                CategoryName = "MockCategory"
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            // Act
            var productBuilder = new ProductBuilder(dictionary, rules);
            var result = productBuilder.BuildProductModel(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("MockProduct", result.Name);
            Assert.AreEqual("MockManufacturer", result.Manufacturer.Name);
            Assert.AreEqual("MockCategory", result.Category.Name);
        }

        [TestMethod]
        public void BuildProductModelTest03()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var product = new Product
            {
                Id = 1,
                Name = "MockProduct",
                Manufacturer = new ProductManufacturer { Id = 1, Name = "MockManufacturer" },
                CategoryId = null,
                CategoryName = "MockCategory",
                Variants = new ProductList
                {
                    new Product {Price = 10, Id = 1, Manufacturer = new ProductManufacturer { PartNo = "555" }, PartNo = "888", OnHand = new OnHand {IsActive = true, Value = 8 }, ImageKey = Guid.Empty},
                    new Product {Price = 12, Id = 1, Manufacturer = new ProductManufacturer { PartNo = "556" }, PartNo = "889", OnHand = new OnHand {IsActive = true, Value = 8 }, ImageKey = Guid.Empty, VariantParametrics = new ProductParametricList { new ProductParametric { Id = 111, ValueId = 37, ValueIdSeed = "1, 2"}}, Parametrics = new ProductParametricList { new ProductParametric { Id = 111, ValueId = 37, ValueIdSeed = "1, 2"}}, FlagIdSeed = "1,2"}
                },
                Parametrics = new ProductParametricList { new ProductParametric { Id = 111, ValueId = 37, ValueIdSeed = "1, 2" } },
                Files = new ProductFileList
                {
                    new ProductFile { Id = (int)ProductFileType.AdditionalImage, Name = "First", Type = (int)FileType.Embedded }, 
                    new ProductFile { Id = (int)ProductFileType.AdditionalImage, Name = "Second", Type = (int)FileType.Jpg }
                },
                Families = new ProductIdNameDescriptionList { new IdNameDescription { Id = 1, Name = "Family", Description = "Stone", ImageKey = Guid.Empty } }
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            dictionary.Stub(x => x.ParametricInfo(111, "sv")).Return(new ParametricInfo { Id = 111, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricInfo(112, "sv")).Return(new ParametricInfo { Id = 112, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Decimal });
            dictionary.Stub(x => x.ParametricInfo(113, "sv")).Return(new ParametricInfo { Id = 113, Type = ParametricType.Value, ValueType = ParametricValueType.Date });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 37, "sv")).Return(new ParametricValue { Id = 37, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
       
            // Act
            var productBuilder = new ProductBuilder(dictionary, rules);
            var result = productBuilder.BuildProductModel(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Families.Count);
            Assert.AreEqual("Family", result.Families[0].Name);
            Assert.AreEqual("Stone", result.Families[0].Description);
            Assert.AreEqual("MockManufacturer", result.Manufacturer.Name);
            Assert.AreEqual(2, result.Files.Count);
            Assert.AreEqual((int)ProductFileType.AdditionalImage, result.Files[1].Type);
            Assert.AreEqual(2, result.Variants.Count);
            Assert.AreEqual(2, result.Variants[1].Flags.Count);
        }
    }
}
