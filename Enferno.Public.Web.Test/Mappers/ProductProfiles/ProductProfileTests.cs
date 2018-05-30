using System;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Enferno.Public.Web.Test.Mappers.ProductProfiles
{
    [TestClass]
    public class ProductProfileTests
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

        [TestMethod, TestCategory("UnitTest")]

        public void MapProductWithFilesTest()
        {
            // Arrange
            var product = new Product
            {
                Files = new ProductFileList
                {
                    new ProductFile { Description = "Desc1", Name = "Name1", Id = 11, Key = Guid.NewGuid(), Path = "file1.jpg", Type = (int)FileType.Jpg},
                    new ProductFile { Description = "Desc2", Name = "Name2", Id = 46, Key = Guid.NewGuid(), Path = "file2.mp4", Type = (int)ExternalFileType.Mp4 },
                    new ProductFile { Description = "Desc3", Name = "Name3", Id = 47, Key = Guid.NewGuid(), Path = "file3.eps", Type = (int)ExternalFileType.Eps }
                },                
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            StormContext.SetInstance(stormContext);      
            // Act

            var model = Mapper.Map<Product, ProductModel>(product);

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(3, model.Files.Count);
            Assert.IsTrue(model.Files[0].Url.EndsWith("jpg"), "jpg");
            Assert.IsTrue(model.Files[1].Url.EndsWith("mp4"), "mp4");
            Assert.IsTrue(model.Files[2].Url.EndsWith("eps"), "eps");
        }


        [TestMethod, TestCategory("UnitTest")]

        public void MapProductItemWithFilesTest()
        {
            // Arrange
            var productItem = new ProductItem
            {
                ImageKey = Guid.NewGuid(),
                AdditionalImageKeySeed = "43:87BE8515-9227-4B9E-B975-A2CB21F6A26E,44:9014C5A7-1BEF-45E9-A259-8B121C440E0E"
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            StormContext.SetInstance(stormContext);
            // Act

            var model = Mapper.Map<ProductItem, ProductItemModel>(productItem);

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(3, model.Files.Count);
            Assert.IsTrue(model.Files[0].Url.EndsWith("jpg"), "jpg");
            Assert.AreEqual(model.Files[1].Type, 43);
            Assert.AreEqual(model.Files[2].Type, 44);
        }

        [TestMethod, TestCategory("UnitTest")]

        public void MapProductItemWithSameFileInAdditionalImagesTest()
        {
            // Arrange
            var productItem = new ProductItem
            {
                ImageKey = new Guid("87be8515-9227-4b9e-b975-a2cb21f6a26e"),
                AdditionalImageKeySeed = "44:9014C5A7-1BEF-45E9-A259-8B121C440E0E,58:87be8515-9227-4b9e-b975-a2cb21f6a26e"
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            StormContext.SetInstance(stormContext);
            // Act

            var model = Mapper.Map<ProductItem, ProductItemModel>(productItem);

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(3, model.Files.Count);
        }
    }

    public class TestSiteRules : DefaultSiteRules
    {
        public override OnHandStatusModel GetOnHandStatus(Product product)
        {
            return new OnHandStatusModel();
        }

        public override OnHandStatusModel GetOnHandStatus(ProductItem productItem)
        {
            return new OnHandStatusModel();
        }

        public override OnHandStatusModel GetOnHandStatus(StormApiClient.Shopping.Basket basket)
        {
            return new OnHandStatusModel();
        }

        public override string GetProductPageUrl(ProductItem product)
        {
            return "fakeUrl";
        }
    }
}
