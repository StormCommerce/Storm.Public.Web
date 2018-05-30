using System;
using System.Collections.Generic;
using Enferno.Public.Web.Builders;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Enferno.Public.Web.Test.Builders
{
    [TestClass]
    public class BuilderBaseTests
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetImageUrlTest01()
        {
            var imageKey = Guid.Empty;
            var result = MockBuilderBase.GetImageUrl(imageKey);
            Assert.AreEqual("http://services.enferno.se/image/00000000-0000-0000-0000-000000000000.jpg", result);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapProductManufacturerTest01()
        {
            // Arrange
            var manufacturer = new ProductManufacturer
            {
                Id = 1,
                Name = "Bob"
            };

            // Act
            var result = MockBuilderBase.MapProductManufacturer(manufacturer);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Bob", result.Name);
            Assert.AreEqual(null, result.ImageUrl);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapProductManufacturerTest02()
        {
            // Arrange
            var manufacturer = new ProductManufacturer
            {
                Id = 1,
                Name = "Bob",
                LogoKey = Guid.Empty
            };

            // Act
            var result = MockBuilderBase.MapProductManufacturer(manufacturer);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Bob", result.Name);
            Assert.AreEqual("http://services.enferno.se/image/00000000-0000-0000-0000-000000000000.jpg", result.ImageUrl);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapFlagsTest01()
        {
            // Arrange
            var flagList = new List<int>();

            // Act
            MockBuilderBase.MapFlags(null, flagList);

            // Assert
            Assert.AreEqual(0, flagList.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapFlagsTest02()
        {
            // Arrange
            const string flagSeed = "1,2,3,5";
            var flagList = new List<int>();

            // Act
            MockBuilderBase.MapFlags(flagSeed, flagList);

            // Assert
            Assert.AreEqual(4, flagList.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapFlagsTest03()
        {
            // Arrange
            const string flagSeed = "1,2,2,5";
            var flagList = new List<int>();

            // Act
            MockBuilderBase.MapFlags(flagSeed, flagList);

            // Assert
            Assert.AreEqual(3, flagList.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapPriceTest01()
        {
            // Arrange
            var product = new Product
            {
                VatRate = 1.25m,
                PriceListId = 1,
                Price = 100
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext); 

            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(1, result.PricelistId);
            Assert.AreEqual(100, result.Display);
            Assert.AreEqual(100, result.Catalog);
            Assert.AreEqual(100, result.Recommended);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapPriceTest02()
        {
            // Arrange
            var product = new Product
            {
                VatRate = 1.25m,
                PriceListId = 1,
                Price = 100
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            StormContext.SetInstance(stormContext); 
                
            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(1, result.PricelistId);
            Assert.AreEqual(125, result.Display);
            Assert.AreEqual(125, result.Catalog);
            Assert.AreEqual(125, result.Recommended);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapPriceTest03()
        {
            // Arrange
            var product = new Product
            {
                VatRate = 1.25m,
                PriceListId = 1,
                Price = 100,
                PriceCatalog = 150,
                PriceRecommended = 200
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            StormContext.SetInstance(stormContext);   


            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(1, result.PricelistId);
            Assert.AreEqual(125, result.Display);
            Assert.AreEqual(187.50m, result.Catalog);
            Assert.AreEqual(250, result.Recommended);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapPriceTest04()
        {
            // Arrange
            var product = new ProductItem
            {
                VatRate = 1.25m,
                PriceListId = 1,
                Price = 100
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);   

            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(1, result.PricelistId);
            Assert.AreEqual(100, result.Display);
            Assert.AreEqual(100, result.Catalog);
            Assert.AreEqual(100, result.Recommended);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapPriceTest05()
        {
            // Arrange
            var product = new ProductItem
            {
                VatRate = 1.25m,
                PriceListId = 1,
                Price = 100
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            StormContext.SetInstance(stormContext);   

            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(1, result.PricelistId);
            Assert.AreEqual(125, result.Display);
            Assert.AreEqual(125, result.Catalog);
            Assert.AreEqual(125, result.Recommended);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MapPriceTest06()
        {
            // Arrange
            var product = new ProductItem
            {
                VatRate = 1.25m,
                PriceListId = 1,
                Price = 100,
                PriceCatalog = 150,
                PriceRecommended = 200
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            StormContext.SetInstance(stormContext);   
            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(1, result.PricelistId);
            Assert.AreEqual(125, result.Display);
            Assert.AreEqual(187.50m, result.Catalog);
            Assert.AreEqual(250, result.Recommended);
        }

        [TestMethod, TestCategory("UnitTest")]

        public void MapPriceTest07()
        {
            // Arrange
            var product = new ProductItem
            {
                VatRate = 1.25m,
                PriceListId = null,
                Price = 100
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);   

            // Act
            var result = MockBuilderBase.MapPrice(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1.25m, result.VatRate);
            Assert.AreEqual(0, result.PricelistId);
            Assert.AreEqual(100, result.Display);
            Assert.AreEqual(100, result.Catalog);
            Assert.AreEqual(100, result.Recommended);
        }  
    }


    internal class MockBuilderBase : BuilderBase
    {
        public MockBuilderBase(ISiteRules rules)
            : base(rules)
        {
        }

        public new static string GetImageUrl(Guid? imageKey)
        {
            return BuilderBase.GetImageUrl(imageKey);
        }

        public new static ProductManufacturerModel MapProductManufacturer(ProductManufacturer manufacturer)
        {
            return BuilderBase.MapProductManufacturer(manufacturer);
        }

        public new static void MapFlags(string flagSeed, ICollection<int> flagList)
        {
            BuilderBase.MapFlags(flagSeed, flagList);
        }

        public new static PriceModel MapPrice(Product product)
        {
            return BuilderBase.MapPrice(product);
        }

        public new static PriceModel MapPrice(ProductItem product)
        {
            return BuilderBase.MapPrice(product);
        }
    }
}
