using System;
using System.Diagnostics;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Managers;
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

namespace Enferno.Public.Web.Test.Managers
{
    [TestClass]
    public class ProductManagerTests
    {

        private ProductManager _productManager;

        private readonly Product _product = new Product
        {
            Id = 1,
            Name = "MockProduct",
            PriceListId = 1,
            Price = 125,
            PriceCatalog = 130,
            VatRate = 1.25m,
            Manufacturer = new ProductManufacturer { Id = 1, Name = "MockManufacturer" },
            CategoryId = null,
            CategoryName = "MockCategory",
            Variants = new ProductList
                {
                    new Product
                    {
                        Price = 10,
                        Id = 1,
                        Manufacturer = new ProductManufacturer {PartNo = "555"},
                        PartNo = "888",
                        OnHand = new OnHand {IsActive = true, Value = 8},
                        ImageKey = Guid.Empty
                    },
                    new Product
                    {
                        Price = 12,
                        Id = 1,
                        Manufacturer = new ProductManufacturer {PartNo = "556"},
                        PartNo = "889",
                        OnHand = new OnHand {IsActive = true, Value = 8},
                        ImageKey = Guid.Empty,
                        VariantParametrics =
                            new ProductParametricList
                            {
                               new ProductParametric {Id = 111, ValueId = 37}
                            },
                        Parametrics =
                            new ProductParametricList
                            {
                                new ProductParametric {Id = 111, ValueId = 37}
                            },
                        FlagIdSeed = "1,2"
                    }
                },
            Parametrics =
                new ProductParametricList { new ProductParametric { Id = 111, ValueId = 37 }},
            Files =
                new ProductFileList
                    {
                        new ProductFile {Id = 44, Name = "First", Type = (int) FileType.Embedded},
                        new ProductFile {Id = 44, Name = "Second", Type = (int) FileType.Jpg}
                    },
            Families =
                new ProductIdNameDescriptionList
                    {
                        new IdNameDescription {Id = 1, Name = "Family", Description = "Stone", ImageKey = Guid.Empty}
                    }
        };

        [TestInitialize]
        public void Initialize()
        {
            var productRepository = MockRepository.GenerateMock<IProductRepository>();
            productRepository.Stub(mock => mock.GetProduct(_product.Id)).Return(_product);

            productRepository.Stub(r => r.ListManufacturers(""))
                .IgnoreArguments()
                .Return(new ManufacturerItemPagedList
                {
                    Items = new ManufacturerItemList {new ManufacturerItem {Id = 3, Name = "Chuck"}}
                });

            productRepository.Stub(r => r.ListParametricInfo("")).IgnoreArguments().Return(new ParametricInfoList
            {
                new ParametricInfo {Id = 111, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer}
            });

            productRepository.Stub(r => r.ListParametricValues("")).IgnoreArguments().Return(new ParametricValueList
            {
                new ParametricValue
                {
                    Id = 37,
                    Name = "Valuee",
                    Description = "Desc",
                    Code = "Coode",
                    SortOrder = 1,
                    Type = ParametricType.ListValue.ToString()
                }
            });

            IoC.RegisterInstance(typeof (IProductRepository), productRepository);

            var containerRepository = MockRepository.GenerateMock<IRepository>();
            containerRepository.Stub(r => r.Products).Return(productRepository);
            IoC.RegisterInstance(typeof (IRepository), containerRepository);

            var siteRules = MockRepository.GenerateMock<ISiteRules>();
            IoC.RegisterInstance(typeof (ISiteRules), siteRules);

            Mapper.AddProfile<ProductProfile>();

            _productManager = new ProductManager();
        }

        [TestMethod]
        public void CanGetProductFromId()
        {
            // Arrange
            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var productsRepository = MockRepository.GenerateMock<IProductRepository>();
            productsRepository.Stub(x => x.ListParametricInfo()).IgnoreArguments()
                .Return(
                new ParametricInfoList
                {
                    new ParametricInfo { Id = 111, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer }
                }
            );

            productsRepository.Stub(x => x.ListParametricValues()).IgnoreArguments()
                .Return(
                new ParametricValueList
                {
                    new ParametricValue { Id = 37, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() }
                }
            );

            var repository = MockRepository.GenerateMock<IRepository>();
            repository.Stub(x => x.Products).Return(productsRepository);

            IoC.Container.RegisterInstance(typeof(IRepository), repository, new PerThreadLifetimeManager());
            ApplicationDictionary.Instance.Refresh();

            var productViewModel = _productManager.GetProduct(_product.Id);

            Assert.IsNotNull(productViewModel);
            Assert.AreEqual(_product.PriceListId, productViewModel.PriceListId);
            Assert.AreEqual(_product.Price, productViewModel.DisplayPrice);
        }
    }
}
