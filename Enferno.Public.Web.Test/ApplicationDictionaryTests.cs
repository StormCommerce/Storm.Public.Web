using System;
using System.Linq;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Repositories;
using Enferno.StormApiClient.Products;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Enferno.Public.Web.Test
{
    [TestClass]
    public class ApplicationDictionaryTests
    {
        protected static readonly string cultureCode = "sv-SE";

        [TestMethod]
        public void ListPricelistsTest()
        {
            // Arrange
            var productsRepository = new Mock<IProductRepository>();
            productsRepository.Setup(x => x.ListPricelists(It.IsAny<string>(), It.IsAny<bool>())).Returns(
                new PricelistList
                {
                    new Pricelist {Id = 1},
                    new Pricelist {Id = 2},
                    new Pricelist {Id = 3},
                }
            );

            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Products).Returns(productsRepository.Object);

            IoC.Container.RegisterInstance(typeof(IRepository), repository.Object, new PerThreadLifetimeManager());
            ApplicationDictionary.Instance.Refresh();
            
            // Act
            var pricelists = ApplicationDictionary.Instance.Pricelists(cultureCode);

            // Assert
            Assert.AreEqual(3, pricelists.Count());
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ApplicationDictionaryWithMissingDictionaryForLanguage()
        {
            // Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.ListParametricInfo("en", true)).Returns(new ParametricInfoList { new ParametricInfo {Id = 1, Name = "Name"} });
            productRepository.Setup(x => x.ListParametricInfo("sv", true)).Returns(new ParametricInfoList { new ParametricInfo {Id = 1, Name = "Namn"} });

            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Products).Returns(productRepository.Object);

            IoC.Container.RegisterInstance(typeof(IRepository), repository.Object, new PerThreadLifetimeManager());
            ApplicationDictionary.Instance.Refresh();

            // Act 1
            var pInfo = ApplicationDictionary.Instance.ParametricInfo(1, "en");
            
            // Assert 1
            Assert.AreEqual("Name", pInfo.Name);

            // Act 2
            pInfo = ApplicationDictionary.Instance.ParametricInfo(1, "sv");

            // Assert 2
            Assert.AreEqual("Namn", pInfo.Name);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ManufacturerReloadNoNewTest()
        {
            // Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.ListManufacturers(null, null, null, "sv", true))
            .Returns(new ManufacturerItemPagedList
            {
                    ItemCount = 4,
                    Items = new ManufacturerItemList
                    {
                        new ManufacturerItem { Id = 1, Name = "Name1"},
                        new ManufacturerItem { Id = 2, Name = "Name2"},
                    }
            });

            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Products).Returns(productRepository.Object);

            IoC.Container.RegisterInstance(typeof(IRepository), repository.Object, new PerThreadLifetimeManager());
            ApplicationDictionary.Instance.Refresh();

            // Act 1
            var data = ApplicationDictionary.Instance.ManufacturerItem(1, "sv");
            // Assert 1
            Assert.AreEqual("Name1", data.Name);

            // Act 2
            data = ApplicationDictionary.Instance.ManufacturerItem(3, "sv");
            // Assert 2
            Assert.IsNull(data);

            // Act 2
            data = ApplicationDictionary.Instance.ManufacturerItem(3, "sv");
            // Assert 2
            Assert.IsNull(data);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void ManufacturerReloadNewTest()
        {
            // Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.SetupSequence(x => x.ListManufacturers(null, null, null, "sv", true))
            .Returns(new ManufacturerItemPagedList
            {
                ItemCount = 4,
                Items = new ManufacturerItemList
                {
                    new ManufacturerItem { Id = 1, Name = "Name1"},
                    new ManufacturerItem { Id = 2, Name = "Name2"},
                }
            })
            .Returns(new ManufacturerItemPagedList
            {
                ItemCount = 4,
                Items = new ManufacturerItemList
                {
                    new ManufacturerItem { Id = 1, Name = "Name1"},
                    new ManufacturerItem { Id = 2, Name = "Name2"},
                    new ManufacturerItem { Id = 3, Name = "Name3"},
                }
            });

            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Products).Returns(productRepository.Object);

            IoC.Container.RegisterInstance(typeof(IRepository), repository.Object, new PerThreadLifetimeManager());
            ApplicationDictionary.Instance.Refresh();

            // Act 1
            var data = ApplicationDictionary.Instance.ManufacturerItem(1, "sv");
            // Assert 1
            Assert.AreEqual("Name1", data.Name);

            // Act 2
            data = ApplicationDictionary.Instance.ManufacturerItem(3, "sv");
            // Assert 2
            Assert.AreEqual("Name3", data.Name);
        }
    }
}
