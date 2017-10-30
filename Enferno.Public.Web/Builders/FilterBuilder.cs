
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Builders
{
    public class FilterBuilder : BuilderBase
    {
        private readonly List<FilterModel> filters = new List<FilterModel>();

        private readonly IApplicationDictionary dictionary;
        public FilterBuilder() : this(ApplicationDictionary.Instance)
        {
            
        }
        public FilterBuilder(IApplicationDictionary dictionary, ISiteRules rules = null) : base(rules)
        {
            this.dictionary = dictionary;
        }

        public FilterBuilder(ISiteRules rules) : base(rules)
        {
            dictionary = ApplicationDictionary.Instance;
        }

        public int? ItemCount { get; set; }

        public IEnumerable<FilterModel> BuildFilters(FilterList list)
        {
            filters.Add(new FilterModel((int)FilterCategory.Category, FilterType.List, ParametricValueType.Integer));
            filters.Add(new FilterModel((int)FilterCategory.Manufacturer, FilterType.List, ParametricValueType.Integer));
            filters.Add(new FilterModel((int)FilterCategory.Flag, FilterType.List, ParametricValueType.Integer));
            filters.Add(new FilterModel((int)FilterCategory.OnHand, FilterType.Bool, ParametricValueType.Boolean));
            filters.Add(new FilterModel((int)FilterCategory.Price, FilterType.Range, ParametricValueType.Decimal));

            AddCategoryValue(list.Find(i => i.Name == "catf"));
            AddManufacturerValue(list.Find(i => i.Name == "mfrf"));
            AddFlagValue(list.Find(i => i.Name == "flgf"));
            AddOnHand(list.Find(i => i.Name == "ohf"));
            AddPriceValue(list.Find(i => i.Name == "prcf"));
            AddParametrics(list.Find(i => i.Name == "parf"));

            filters.RemoveAll(f => !f.HasValidCount(ItemCount));
            return filters;
        }

        private void AddCategoryValue(Filter filter)
        {
            if (filter == null) return;

            var categoryFilter = filters.Find(f => f.Id == (int)FilterCategory.Category);
            foreach (FilterItem item in filter.Items)
            {
                var id = int.Parse(item.Id);
                var category = dictionary.CategoryItem(id, StormContext.CultureCode);
                var value = new FilterValueModel<int>
                {
                    Id = id,
                    Value = int.Parse(item.Value),
                    Name = category.Name,
                    Description = category.Description,
                    ImageUrl = GetImageUrl(category.ImageKey),
                    Code = item.Code,
                    Count = item.Count
                };
                categoryFilter.Values.Add(value);
            }
        }

        private void AddManufacturerValue(Filter filter)
        {
            if (filter == null) return;

            var manufacturerFilter = filters.Find(f => f.Id == (int)FilterCategory.Manufacturer);
            foreach (FilterItem item in filter.Items)
            {
                var id = int.Parse(item.Id);
                var manufacturer = dictionary.ManufacturerItem(id, StormContext.CultureCode);
                var value = new FilterValueModel<int>
                {
                    Id = id,
                    Value = int.Parse(item.Value),
                    Name = manufacturer.Name,
                    Code = item.Code,
                    Count = item.Count
                };
                manufacturerFilter.Values.Add(value);
            }
        }

        private void AddFlagValue(Filter filter)
        {
            if (filter == null) return;

            var flagFilter = filters.Find(f => f.Id == (int)FilterCategory.Flag);
            foreach (FilterItem item in filter.Items)
            {
                var id = int.Parse(item.Id);
                var value = new FilterValueModel<int>
                {
                    Id = id,
                    Value = int.Parse(item.Value),
                    Name = item.Name,
                    Code = item.Code,
                    Count = item.Count
                };
                flagFilter.Values.Add(value);
            }
        }

        private void AddOnHand(Filter filter)
        {
            if (filter == null) return;

            var onHandFilter = filters.Find(f => f.Id == (int)FilterCategory.OnHand);
            var item = filter.Items.FirstOrDefault() as FilterItem;
            if (item == null) return;
            var value = new FilterValueModel<bool>
            {
                Id = (int)FilterCategory.OnHand,
                Value = true,
                Count = item.Count
            };
            onHandFilter.Values.Add(value);
        }

        private void AddPriceValue(Filter filter)
        {
            if (filter == null) return;

            var priceFilter = filters.Find(f => f.Id == (int)FilterCategory.Price);
            var item = filter.Items.FirstOrDefault() as FilterPriceItem;
            if (item == null) return;
            var value = new RangeFilterValueModel<decimal>
            {
                Id = (int)FilterCategory.Price,
                From = StormContext.ShowPricesIncVat.GetValueOrDefault(true) ? item.FromIncVat : item.From,
                To = StormContext.ShowPricesIncVat.GetValueOrDefault(true) ? item.ToIncVat : item.To,
            };
            priceFilter.Values.Add(value);
        }

        private void AddParametrics(Filter filter)
        {
            if (filter == null) return;

            foreach (var item in filter.Items)
            {
                if (item is FilterMultiItem) AddParametrics(item as FilterMultiItem);
                if (item is FilterListItem) AddParametrics(item as FilterListItem);
                if (item is FilterBoolItem) AddParametrics(item as FilterBoolItem);
                if (item is FilterRangeItem) AddParametrics(item as FilterRangeItem);
            }
        }

        private void AddParametrics(FilterMultiItem filterItem)
        {
            var id = int.Parse(filterItem.Id);
            var pi = dictionary.ParametricInfo(id, StormContext.CultureCode);
            
            var type = MapParametricType(pi);
            var filter = new FilterModel(id, type, pi.ValueType, pi.Name, pi.Description, pi.Uom);
            filters.Add(filter);

            foreach (FilterItem item in filterItem.Items)
            {
                var pid = int.Parse(item.Id);
                var pv = dictionary.ParametricValue(pi.Type, pid, StormContext.CultureCode);
                var value = new FilterValueModel<int>
                {
                    Id = pv.Id,
                    Value = pv.Id,
                    Name = pv.Name,
                    Description = pv.Description,
                    Code = pv.Code,
                    SortOrder = pv.SortOrder,
                    ImageUrl = GetImageUrl(pv.ImageKey),
                    Count = item.Count
                };
                filter.Values.Add(value);  
            }
        }

        private void AddParametrics(FilterListItem filterItem)
        {
            var id = int.Parse(filterItem.Id);
            var pi = dictionary.ParametricInfo(id, StormContext.CultureCode);

            var type = MapParametricType(pi);
            var filter = new FilterModel(id, type, pi.ValueType, pi.Name, pi.Description, pi.Uom);
            filters.Add(filter);

            foreach (FilterItem item in filterItem.Items)
            {
                var pid = int.Parse(item.Id);
                var pv = dictionary.ParametricValue(pi.Type, pid, StormContext.CultureCode);
                var value = new FilterValueModel<int>
                {
                    Id = pv.Id,
                    Value = pv.Id,
                    Name = pv.Name,
                    Description = pv.Description,
                    Code = pv.Code,
                    SortOrder = pv.SortOrder,
                    ImageUrl = GetImageUrl(pv.ImageKey),
                    Count = item.Count
                };
                filter.Values.Add(value);
            }
        }

        private void AddParametrics(FilterBoolItem filterItem)
        {
            var id = int.Parse(filterItem.Id);
            var pi = dictionary.ParametricInfo(id, StormContext.CultureCode);

            var type = MapParametricType(pi);
            var filter = new FilterModel(id, type, pi.ValueType, pi.Name, pi.Description, pi.Uom);
            filters.Add(filter);

            var value = new FilterValueModel<bool>
            {
                Id = id,
                Value = true,
                Count = filterItem.Count
            };
            filter.Values.Add(value);
        }

        private void AddParametrics(FilterRangeItem filterItem)
        {
            var id = int.Parse(filterItem.Id);
            var pi = dictionary.ParametricInfo(id, StormContext.CultureCode);

            var type = MapParametricType(pi);
            var filter = new FilterModel(id, type, pi.ValueType, pi.Name, pi.Description, pi.Uom);
            filters.Add(filter);

            CreateAndAddRangeValue(pi, filter, filterItem);            
        }

        private static void CreateAndAddRangeValue(ParametricInfo pi, FilterModel filter, FilterRangeItem filterItem)
        {
            if (pi.ValueType == ParametricValueType.Integer)
            {
                var from = int.Parse(filterItem.From);
                var to = int.Parse(filterItem.To);
                CreateAndAddRangerFilter(from, to, filterItem, filter);
            }
            else if (pi.ValueType == ParametricValueType.Decimal)
            {
                var from = XmlConvert.ToDecimal(filterItem.From);
                var to = XmlConvert.ToDecimal(filterItem.To);
                CreateAndAddRangerFilter(from, to, filterItem, filter);
            }
            else
            {
                var from = XmlConvert.ToDateTime(filterItem.From, XmlDateTimeSerializationMode.Local);
                var to = XmlConvert.ToDateTime(filterItem.To, XmlDateTimeSerializationMode.Local);
                CreateAndAddRangerFilter(from, to, filterItem, filter);
            }
        }

        private static void CreateAndAddRangerFilter<T>(T from, T to, FilterRangeItem rangeItem, FilterModel filter) where T : IComparable
        {
            var fvm = new RangeFilterValueModel<T>(from, to, rangeItem);
            filter.Values.Add(fvm);
        }
    
        private static FilterType MapParametricType(ParametricInfo pi)
        {
            if (pi.Type == ParametricType.ListValue || pi.Type == ParametricType.MultiValue) return FilterType.List;
            if (pi.ValueType == ParametricValueType.Boolean) return FilterType.Bool;
            if (pi.ValueType == ParametricValueType.Html || pi.ValueType == ParametricValueType.Text) return FilterType.List;
            return FilterType.Range;
        }
    }
}
