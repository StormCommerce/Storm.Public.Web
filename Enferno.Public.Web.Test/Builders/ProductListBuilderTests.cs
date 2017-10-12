
using System;
using System.Linq;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Builders;
using Enferno.Public.Web.Mappers.ProductProfiles;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ParametricValue = Enferno.StormApiClient.Products.ParametricValue;

namespace Enferno.Public.Web.Test.Builders
{
    [TestClass]
    public class ProductListBuilderTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var rules = MockRepository.GenerateMock<ISiteRules>();
            IoC.RegisterInstance(typeof(ISiteRules), rules);
            Mapper.Initialize(cfg => cfg.AddProfile<ProductProfile>());
//            Mapper.AddProfile<ProductProfile>();
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListWithNullList()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(null);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListWithEmptyList()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                Items = new ProductItemList()
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreNotEqual(null, result);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductList03()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                ItemCount =  1,
                Items = new ProductItemList
                {
                    new ProductItem
                    {
                        Manufacturer = new ProductManufacturer{Id = 1, Name = "MockManufacturer" }
                    }
                }
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreEqual(0, result.Filters.Count);
            Assert.AreEqual(1, result.ItemCount);
            Assert.AreEqual("MockManufacturer", result.Items[0].Manufacturer.Name);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductList04()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                Items = new ProductItemList
                {
                    new ProductItem
                    {
                        Id = 22,
                        Name = "Bananahammock",
                        CategoryId = 25,
                        Manufacturer = new ProductManufacturer{Id = 1, Name = "MockManufacturer" },
                        AdditionalImageKeySeed = "14:" + Guid.Empty,
                        FlagIdSeed = "1,3,5",
                        VariantParametricSeed = "112",
                        ParametricListSeed = "111:10,111:12,111:14",
                        ParametricMultipleSeed = "112:20,112:22,112:24",
                        ParametricValueSeed = "113:30,113:32,113:34"
                    }
                },
                ItemCount = 1
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            dictionary.Stub(x => x.ParametricInfo(111, "sv")).Return(new ParametricInfo { Id = 111, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricInfo(112, "sv")).Return(new ParametricInfo { Id = 112, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Decimal });
            dictionary.Stub(x => x.ParametricInfo(113, "sv")).Return(new ParametricInfo { Id = 113, Type = ParametricType.Value, ValueType = ParametricValueType.Text });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 10, "sv")).Return(new ParametricValue { Id = 10, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 12, "sv")).Return(new ParametricValue { Id = 12, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 14, "sv")).Return(new ParametricValue { Id = 14, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 20, "sv")).Return(new ParametricValue { Id = 20, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 22, "sv")).Return(new ParametricValue { Id = 22, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 24, "sv")).Return(new ParametricValue { Id = 24, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 30, "sv")).Return(new ParametricValue { Id = 30, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 32, "sv")).Return(new ParametricValue { Id = 32, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 34, "sv")).Return(new ParametricValue { Id = 34, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreNotEqual(null, result);
            Assert.AreEqual(1, result.ItemCount);
            Assert.AreEqual(1, result.Items.Count);
            var productItemModel = result.Items[0];
            Assert.AreEqual("MockManufacturer", productItemModel.Manufacturer.Name);
            Assert.AreEqual(1, productItemModel.Files.Count, "Files");
            Assert.AreEqual("Bananahammock", productItemModel.Files[0].AltText); 
            Assert.AreEqual(3, productItemModel.Flags.Count);
            Assert.AreEqual(5, productItemModel.Flags[2]);
            Assert.AreEqual(2, productItemModel.Parametrics.Count, "Parametrics");
            Assert.AreEqual(111, productItemModel.Parametrics[0].Id);
            Assert.AreEqual(3, productItemModel.Parametrics[0].Values.Count);
            Assert.AreEqual("Valuee", productItemModel.Parametrics[0].Values[0].Name);
            Assert.AreEqual(1, productItemModel.Variants.Count, "Parametrics");
            Assert.AreEqual(0, productItemModel.Variants[0].Parametrics.Count, "Parametrics");
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListProductParametersThatHaveDifferingValuesAmongVariantsShouldBeMovedToVariants()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                Items = new ProductItemList
                {
                    new ProductItem
                    {
                        Id = 26891520,
                        VariantParametricSeed = "6577,6688",
                        ParametricListSeed = "6577:22557,6688:28196",
                        ParametricMultipleSeed = "6681:5801",
                        ParametricValueSeed = "6644:14,6645:505,6646:10,6647:21"
                    },
                    new ProductItem
                    {
                        Id = 26891521,
                        VariantParametricSeed = "6577,6688",
                        ParametricListSeed = "6577:22557,6688:28192",
                        ParametricMultipleSeed = "6681:5801",
                        ParametricValueSeed = "6644:14,6645:505,6646:20,6647:23"
                    },
                    new ProductItem
                    {
                        Id = 26891522,
                        VariantParametricSeed = "6577,6688",
                        ParametricListSeed = "6577:22557,6688:28265",
                        ParametricMultipleSeed = "6681:5800,6681:5801",
                        ParametricValueSeed = "6644:14,6645:505,6646:20,6647:27"
                    },
                },
                ItemCount = 1
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            dictionary.Stub(x => x.ParametricInfo(6577, "sv")).Return(new ParametricInfo { Id = 6577, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricInfo(6688, "sv")).Return(new ParametricInfo { Id = 6688, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricInfo(6681, "sv")).Return(new ParametricInfo { Id = 6681, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Decimal });
            dictionary.Stub(x => x.ParametricInfo(6644, "sv")).Return(new ParametricInfo { Id = 6644, Type = ParametricType.Value, ValueType = ParametricValueType.Text });
            dictionary.Stub(x => x.ParametricInfo(6645, "sv")).Return(new ParametricInfo { Id = 6645, Type = ParametricType.Value, ValueType = ParametricValueType.Text });
            dictionary.Stub(x => x.ParametricInfo(6646, "sv")).Return(new ParametricInfo { Id = 6646, Type = ParametricType.Value, ValueType = ParametricValueType.Text });
            dictionary.Stub(x => x.ParametricInfo(6647, "sv")).Return(new ParametricInfo { Id = 6647, Type = ParametricType.Value, ValueType = ParametricValueType.Text });

            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 22557, "sv")).Return(new ParametricValue { Id = 22557, Name = "L22557", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 28196, "sv")).Return(new ParametricValue { Id = 28196, Name = "L28196", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 28265, "sv")).Return(new ParametricValue { Id = 28265, Name = "L28265", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 5800, "sv")).Return(new ParametricValue { Id = 5800, Name = "MV5800", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 5801, "sv")).Return(new ParametricValue { Id = 5801, Name = "MV5801", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });

            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 14, "sv")).Return(new ParametricValue { Id = 14, Name = "V14", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 505, "sv")).Return(new ParametricValue { Id = 505, Name = "V505", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 10, "sv")).Return(new ParametricValue { Id = 10, Name = "V10", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 21, "sv")).Return(new ParametricValue { Id = 21, Name = "V21", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 20, "sv")).Return(new ParametricValue { Id = 20, Name = "V20", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 23, "sv")).Return(new ParametricValue { Id = 23, Name = "V23", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.Value, 27, "sv")).Return(new ParametricValue { Id = 27, Name = "V27", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.Value.ToString() });

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Items.Count);
            var p = result.Items[0];
            Assert.AreEqual(2, p.Parametrics.Count);
            Assert.IsFalse(p.Parametrics.Exists(x => x.Id == 6681), "6681 should not exists among product-parameters");

            Assert.AreEqual(3, p.Variants.Count);

            {
                var v1 = p.Variants[0];
                Assert.AreEqual(26891520, v1.Id);
                Assert.AreEqual(3, v1.Parametrics.Count);
            }

            {
                var v1 = p.Variants[1];
                Assert.AreEqual(26891521, v1.Id);
                Assert.AreEqual(3, v1.Parametrics.Count);
            }

            {
                var v3 = p.Variants[2];
                Assert.AreEqual(26891522, v3.Id);
                var param = v3.Parametrics.SingleOrDefault(x => x.Id == 6681);
                Assert.IsNotNull(param, "6681 should exist among variant-parameters");
                Assert.AreEqual(2, param.Values.Count);
            }                  
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListFromVariantItems()
        {
            // Arrange
            var rules = new DefaultSiteRules();

            var productList = new ProductItemPagedList
            {
                ItemCount = 1,
                Items = new ProductItemList
                {
                    new ProductItem
                    {
                        Id = 1,
                        Name = "Handske",
                        CategoryId = 1,
                        Manufacturer = new ProductManufacturer{ Id = 1, Name = "Manufacturer" },
                        AdditionalImageKeySeed = "14:" + Guid.Empty,
                        FlagIdSeed = "",
                        Price = 100.00M, PriceCatalog = 100.00M, VatRate = 1.25M, RecommendedQuantity = 1.000M,
                        OnHand = new OnHand { Value = 10, IsActive = true },
                        OnHandStore = new OnHand { IsActive = false },
                        OnHandSupplier = new OnHand { IsActive = false },
                        Key = Guid.NewGuid(),
                        ImageKey = Guid.NewGuid(),
                        StatusId = 1,
                        Type = 1,
                        GroupByKey = "v1",
                        ParametricListSeed = "111:11,112:12,200:20,300:30",
                        ParametricMultipleSeed = "113:13,113:14",
                        ParametricValueSeed = "115:15,116:16,117:17,117:18",
                        VariantParametricSeed = "200,300",
                    }
                }
            };

            var variantList = new VariantItemList
            {
                new VariantItem
                {
                    Id = 1, GroupByKey = "v1", Name = "Handske 1", TypeId = 1, PartNo = "P1", ManufacturerPartNo = "P1", FlagIdSeed = "1", ImageKey = productList.Items[0].ImageKey,
                    Price = new ProductPrice { Value = 100.00M, Catalog = 100.00M, VatRate = 1.25M, PriceListId = 1, IsBuyable = true, RecommendedQuantity = 1, IsRecommendedQuantityFixed = true },
                    OnHand = new OnHand {  Value = 3, IsActive = true },
                    Parametrics = new ParametricsSeed { ListSeed = "200:20,300:30", MultipleSeed = "117:17", ValueSeed = ""},
                    AdditionalImageKeySeed = "58:" + Guid.NewGuid()
                },
                new VariantItem
                {
                    Id = 2, GroupByKey = "v1", Name = "Handske 2", TypeId = 1, PartNo = "P2", ManufacturerPartNo = "P2", FlagIdSeed = "2", ImageKey = Guid.NewGuid(),
                    Price = new ProductPrice { Value = 100.00M, Catalog = 100.00M, VatRate = 1.25M, PriceListId = 1, IsBuyable = true, RecommendedQuantity = 1, IsRecommendedQuantityFixed = true },
                    OnHand = new OnHand {  Value = 7, IsActive = true },
                    Parametrics = new ParametricsSeed { ListSeed = "200:21,300:31", MultipleSeed = "117:18", ValueSeed = ""},
                    AdditionalImageKeySeed = "58:" + Guid.NewGuid(),
                },
            };


            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            dictionary.Stub(x => x.ParametricInfo(111, "sv")).Return(new ParametricInfo { Id = 111, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 11, "sv")).Return(new ParametricValue { Id = 11, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(112, "sv")).Return(new ParametricInfo { Id = 112, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 12, "sv")).Return(new ParametricValue { Id = 12, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(113, "sv")).Return(new ParametricInfo { Id = 113, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 13, "sv")).Return(new ParametricValue { Id = 13, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 14, "sv")).Return(new ParametricValue { Id = 14, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(115, "sv")).Return(new ParametricInfo { Id = 115, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 15, "sv")).Return(new ParametricValue { Id = 15, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(116, "sv")).Return(new ParametricInfo { Id = 116, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 16, "sv")).Return(new ParametricValue { Id = 16, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(200, "sv")).Return(new ParametricInfo { Id = 200, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 20, "sv")).Return(new ParametricValue { Id = 20, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 21, "sv")).Return(new ParametricValue { Id = 21, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(300, "sv")).Return(new ParametricInfo { Id = 300, Type = ParametricType.ListValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 30, "sv")).Return(new ParametricValue { Id = 30, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 31, "sv")).Return(new ParametricValue { Id = 31, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });

            dictionary.Stub(x => x.ParametricInfo(117, "sv")).Return(new ParametricInfo { Id = 117, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Integer });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 17, "sv")).Return(new ParametricValue { Id = 17, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            dictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 18, "sv")).Return(new ParametricValue { Id = 18, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList, variantList);

            // Assert
            Assert.AreNotEqual(null, result);
            Assert.AreEqual(1, result.ItemCount);
            Assert.AreEqual(1, result.Items.Count);

            var product = result.Items[0];
            Assert.AreEqual("Manufacturer", product.Manufacturer.Name);
            Assert.AreEqual(2, product.Files.Count, "Files");
            Assert.AreEqual("Handske", product.Files[0].AltText);
            Assert.AreEqual(0, product.Flags.Count);
            Assert.AreEqual(5, product.Parametrics.Count, "Parametrics");

            Assert.AreEqual(2, product.Variants.Count, "Parametrics");
            var variant1 = product.Variants[0];
            Assert.AreEqual(1, variant1.Parametrics.Count, "Parametrics");
            Assert.AreEqual(2, variant1.VariantParametrics.Count, "VariantParametrics");
            Assert.AreEqual(1, variant1.Flags.Count);
            Assert.AreEqual(2, variant1.Files.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListProductWithNoVariantsImageTest()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                ItemCount = 1,
                Items = new ProductItemList
                {
                    new ProductItem
                    {
                        ImageKey = Guid.NewGuid(),
                        VariantImageKey = null
                    }
                }
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreEqual(1, result.Items[0].Files.Count);
            Assert.AreEqual(0, result.Items[0].Variants[0].Files.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListProductWithOneVariantsImageTest()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                ItemCount = 1,
                Items = new ProductItemList
                {
                    new ProductItem
                    {
                        ImageKey = Guid.NewGuid(),
                        VariantImageKey = Guid.NewGuid()
                    }
                }
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreEqual(1, result.Items[0].Files.Count);
            Assert.AreEqual(1, result.Items[0].Variants[0].Files.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListProductWithNoAdditionalImageWhenVariantSpecificTest()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                ItemCount = 1,
                Items = new ProductItemList
                {
                    new ProductItem { ImageKey = Guid.NewGuid(), VariantImageKey = Guid.NewGuid(), AdditionalImageKeySeed = "58:11111111-e238-476d-9886-9f2193539d2a"},
                    new ProductItem { ImageKey = Guid.NewGuid(), VariantImageKey = Guid.NewGuid(), AdditionalImageKeySeed = "58:22222222-e238-476d-9886-9f2193539d2a"},
                }
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreEqual(1, result.Items[0].Files.Count);
            Assert.AreEqual(2, result.Items[0].Variants[0].Files.Count);
            Assert.AreEqual(2, result.Items[0].Variants[1].Files.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListWithAllClosedItemsAndZeroPrice()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                ItemCount = 1,
                Items = new ProductItemList
                {
                    new ProductItem { Id = 1, PartNo = "1", GroupByKey = "1", Price = 0, PriceListId = null, StatusId = 5 },
                    new ProductItem { Id = 2, PartNo = "2", GroupByKey = "1", Price = 0, PriceListId = null, StatusId = 5 },
                }
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreEqual(1, result.ItemCount);
            var product = result.Items[0];
            Assert.AreEqual(0, product.Price.Display);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void BuildProductListWithSomeClosedItemsAndPrice()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var productList = new ProductItemPagedList
            {
                ItemCount = 1,
                Items = new ProductItemList
                {
                    new ProductItem { Id = 1, PartNo = "1", GroupByKey = "1", Price = 1, PriceListId = 1, StatusId = 1 },
                    new ProductItem { Id = 2, PartNo = "2", GroupByKey = "1", Price = 0, PriceListId = null, StatusId = 5 },
                }
            };

            var dictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(false);
            StormContext.SetInstance(stormContext);

            // Act
            var productBuilder = new ProductListBuilder(dictionary, rules);
            var result = productBuilder.BuildProductList(productList);

            // Assert
            Assert.AreEqual(1, result.ItemCount);
            var product = result.Items[0];
            Assert.AreEqual(1, product.Price.Display);
            Assert.AreEqual(false, product.Price.IsFromPrice);
        }
    }
}

