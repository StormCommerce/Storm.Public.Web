
using System;
using System.Linq;
using Enferno.Public.Web.Builders;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Filter = Enferno.StormApiClient.Products.Filter;
using FilterItem = Enferno.StormApiClient.Products.FilterItem;
using FilterPriceItem = Enferno.StormApiClient.Products.FilterPriceItem;
using ParametricValue = Enferno.StormApiClient.Products.ParametricValue;

namespace Enferno.Public.Web.Test.Builders
{
    [TestClass]
    public class FilterBuilderTests
    {
        [TestMethod]
        public void BuildFiltersTest01()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();
            var applicationDictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            var filterList = new FilterList();            
            
            // Act
            var filterBuilder = new FilterBuilder(applicationDictionary, rules);
            var result = filterBuilder.BuildFilters(filterList);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void BuildFiltersTest02()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();

            var filterList = new FilterList
            {
                new Filter {Name = "catf", Items = new FilterItemList {new FilterItem {Code = "Category1", Count = 1, Id = "1", Value = "1" }, new FilterItem {Code = "Category2", Count = 1, Id = "2", Value = "2" }}},
                new Filter {Name = "mfrf", Items = new FilterItemList {new FilterItem {Code = "Mfr1", Count = 1, Id = "2", Value = "1" }, new FilterItem {Code = "Mfr2", Count = 1, Id = "2", Value = "2" }}},
                new Filter {Name = "flgf", Items = new FilterItemList {new FilterItem {Code = "Flag1", Count = 1, Id = "3", Value = "1" },new FilterItem {Code = "Flag2", Count = 1, Id = "3", Value = "2" }}},
                new Filter {Name = "ohf", Items = new FilterItemList {new FilterItem {Code = "Ohf", Count = 1, Id = "4", Value = "4" }}},
                new Filter {Name = "prcf", Items = new FilterItemList {new FilterPriceItem {Id = "5", Name = "Price", From = 10.0m, FromIncVat = 12.5m, To = 100.0m, ToIncVat = 125.0m}}},
                new Filter {Name = "parf", Items = new FilterItemList
                {
                    new FilterItem {Code = "Bob", Count = 1, Id = "11", Value = "6" },
                    new FilterMultiItem {Id = "111", Name = "Bob", Description = "Dobalina", ImageKey = Guid.Empty, Items = new FilterItemList {new FilterItem {Id = "37", Count = 1}}},
                    new FilterListItem {Id = "112", Name = "Bob", Description = "Dobalina", ImageKey = Guid.Empty, Items = new FilterItemList {new FilterItem {Id = "48", Count = 2}}},
                    new FilterBoolItem {Id = "113", Name = "Bob", Description = "Dobalina", ImageKey = Guid.Empty, Count = 1, FalseCount = 2},
                    new FilterRangeItem {Id = "114", Name = "Bob", Description = "Dobalina", ImageKey = Guid.Empty, From = "10", To = "20", Type = "ParametricValueType.Integer", Uom = "2"},
                    new FilterRangeItem {Id = "115", Name = "Bob", Description = "Dobalina", ImageKey = Guid.Empty, From = "1.5", To = "4.5", Type = "ParametricValueType.Decimal", Uom = "2"},
                    new FilterRangeItem {Id = "116", Name = "Bob", Description = "Dobalina", ImageKey = Guid.Empty, From = "2014-03-01", To = "2014-03-01", Type = "ParametricValueType.Date", Uom = "2"}
                }},
                new Filter {Name = "bogus"} // Not valid
            };
          
            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var applicationDictionary = MockRepository.GenerateMock<IApplicationDictionary>();
            applicationDictionary.Stub(x => x.ParametricInfo(111, "sv")).Return(new ParametricInfo { Id = 111, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Text });
            applicationDictionary.Stub(x => x.ParametricInfo(112, "sv")).Return(new ParametricInfo { Id = 112, Type = ParametricType.ListValue, ValueType = ParametricValueType.Text });
            applicationDictionary.Stub(x => x.ParametricInfo(113, "sv")).Return(new ParametricInfo { Id = 113, Type = ParametricType.Value, ValueType = ParametricValueType.Boolean });
            applicationDictionary.Stub(x => x.ParametricInfo(114, "sv")).Return(new ParametricInfo { Id = 114, Type = ParametricType.MultiValue, ValueType = ParametricValueType.Integer });
            applicationDictionary.Stub(x => x.ParametricInfo(115, "sv")).Return(new ParametricInfo { Id = 115, Type = ParametricType.ListValue, ValueType = ParametricValueType.Decimal });
            applicationDictionary.Stub(x => x.ParametricInfo(116, "sv")).Return(new ParametricInfo { Id = 116, Type = ParametricType.ListValue, ValueType = ParametricValueType.Date });

            applicationDictionary.Stub(x => x.ParametricValue(ParametricType.MultiValue, 37, "sv")).Return(new ParametricValue { Id = 37, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.MultiValue.ToString() });
            applicationDictionary.Stub(x => x.ParametricValue(ParametricType.ListValue, 48, "sv")).Return(new ParametricValue { Id = 48, Name = "Valuee", Description = "Desc", Code = "Coode", SortOrder = 1, Type = ParametricType.ListValue.ToString() });

            applicationDictionary.Stub(x => x.ManufacturerItem(1, "sv")).Return(new ManufacturerItem { Id = 1, Name = "Mfr1" });
            applicationDictionary.Stub(x => x.ManufacturerItem(2, "sv")).Return(new ManufacturerItem { Id = 2, Name = "Mfr2" });
            applicationDictionary.Stub(x => x.CategoryItem(1, "sv")).Return(new CategoryItem { Id = 1, CategoryId = 1, Name = "Category1", Description = "Category1" });
            applicationDictionary.Stub(x => x.CategoryItem(2, "sv")).Return(new CategoryItem { Id = 2, CategoryId = 2, Name = "Category2", Description = "Category2" });  

            // Act
            var filterBuilder = new FilterBuilder(applicationDictionary, rules);
            filterBuilder.ItemCount = 2;
            var result = filterBuilder.BuildFilters(filterList).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(9, result.Count);
            Assert.AreEqual(2, result[0].Values.Count);
            Assert.AreEqual(FilterType.List, result[0].Type);
            Assert.AreEqual(ParametricValueType.Integer, result[0].ValueType);
            Assert.AreEqual(FilterType.Bool, result[3].Type);
            Assert.AreEqual(ParametricValueType.Boolean, result[3].ValueType);
            Assert.AreEqual(FilterType.Range, result[4].Type);
            Assert.AreEqual(ParametricValueType.Decimal, result[4].ValueType);

            Assert.AreEqual(111, result[5].Id);
            Assert.AreEqual(113, result[6].Id);
            Assert.AreEqual(114, result[7].Id);
            Assert.AreEqual(115, result[8].Id);
        }

        [TestMethod]
        public void BuildFiltersWhenListHasOneWithCountEqualsItemCountTest()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();

            var filterList = new FilterList
            {
                new Filter {Name = "flgf", Items = new FilterItemList {new FilterItem {Code = "Flag1", Count = 1, Id = "3", Value = "1" },new FilterItem {Code = "Flag2", Count = 2, Id = "3", Value = "2" }}},
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var applicationDictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            // Act
            var filterBuilder = new FilterBuilder(applicationDictionary, rules);
            filterBuilder.ItemCount = 2;
            var result = filterBuilder.BuildFilters(filterList).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);          
        }

        [TestMethod]
        public void BuildFiltersWhenListHasAllWithCountEqualsItemCountTest()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();

            var filterList = new FilterList
            {
                new Filter {Name = "flgf", Items = new FilterItemList {new FilterItem {Code = "Flag1", Count = 2, Id = "3", Value = "1" },new FilterItem {Code = "Flag2", Count = 2, Id = "3", Value = "2" }}},
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var applicationDictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            // Act
            var filterBuilder = new FilterBuilder(applicationDictionary, rules);
            filterBuilder.ItemCount = 2;
            var result = filterBuilder.BuildFilters(filterList).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void BuildFiltersWhenListHasMixedWithCountEqualsItemCountTest()
        {
            // Arrange
            var rules = MockRepository.GenerateMock<ISiteRules>();

            var filterList = new FilterList
            {
                new Filter {Name = "flgf", Items = new FilterItemList
                {
                    new FilterItem {Code = "Flag0", Count = 0, Id = "3", Value = "0" },
                    new FilterItem {Code = "Flag1", Count = 1, Id = "3", Value = "1" },
                    new FilterItem {Code = "Flag2", Count = 2, Id = "3", Value = "2" }
                }},
            };

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);

            var applicationDictionary = MockRepository.GenerateMock<IApplicationDictionary>();

            // Act
            var filterBuilder = new FilterBuilder(applicationDictionary, rules);
            filterBuilder.ItemCount = 2;
            var result = filterBuilder.BuildFilters(filterList).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Values.Count);
        }
    }
}
