using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Microsoft.Practices.ObjectBuilder2;

namespace Enferno.Public.Web.Builders
{
    public class ProductListBuilder : BuilderBase
    {
        private ProductListModel model;

        private readonly IApplicationDictionary dictionary;

        public ProductListBuilder() : this(ApplicationDictionary.Instance)
        {            
        }

        public ProductListBuilder(IApplicationDictionary dictionary, ISiteRules rules = null) : base(rules)
        {
            this.dictionary = dictionary;
        }

        public ProductListBuilder(ISiteRules rules) : base(rules)
        {            
            dictionary = ApplicationDictionary.Instance;
        }

        public ProductListModel BuildProductList(ProductItemPagedList productList, FilterList filters = null)
        {
            return productList == null ? null : MapProductList(productList, null, filters);
        }

        public ProductListModel BuildProductList(ProductItemPagedList productList, VariantItemList variants, FilterList filters = null)
        {
            return productList == null ? null :  MapProductList(productList, variants, filters);
        }

        private ProductListModel MapProductList(ProductItemPagedList productList, VariantItemList variants, FilterList filters)
        {
            model = new ProductListModel
            {
                ItemCount = productList.ItemCount,
                Items = new List<ProductItemModel>(),                
                Filters = new List<FilterModel>(),
            };

            if (productList.ItemCount == 0) return model;

            if (filters != null && filters.Count > 0)
            {
                var filterBuilder = new FilterBuilder(dictionary, SiteRules);
                model.Filters.AddRange(filterBuilder.BuildFilters(filters));
            }

            model.Items = MapProductItems(productList.Items, variants);
            return model;
        }

        private List<ProductItemModel> MapProductItems(List<ProductItem> productItems, VariantItemList variants)
        {
            var productGroup = productItems.GroupBy(i => i.GroupByKey);
            return variants == null
                ? productGroup.Select(MapProductItemModel).ToList()
                : productGroup.Select(g => MapProductItemModel(g.First(), variants)).ToList();
        }

        private ProductItemModel MapProductItemModel(IGrouping<string, ProductItem> productItemGroup)
        {
            var productItem = productItemGroup.First();
            var itemModel = Mapper.Map<ProductItem, ProductItemModel>(productItem);

            MapVariants(productItemGroup, itemModel);
            MapProductParametrics(itemModel, productItem);

            var activeItems = productItemGroup.Where(i => i.StatusId != 5).ToList();
            itemModel.Price = MapMinPrice(activeItems);
            itemModel.Price.IsFromPrice = activeItems.Any() && activeItems.Max(v => v.Price) != activeItems.Min(v => v.Price);

            itemModel.OnHandStatus = itemModel.Variants.Min(x => x.OnHandStatus);

            return itemModel;
        }

        private ProductItemModel MapProductItemModel(ProductItem productItem, VariantItemList allVariants)
        {
            var itemModel = Mapper.Map<ProductItem, ProductItemModel>(productItem);

            var variants = allVariants.Where(v => v.GroupByKey == productItem.GroupByKey).ToList();
            MapVariants(productItem, variants, itemModel);

            MoveEqualParametricsFromVariantToProduct(itemModel);
            var allParametrics = GetAllParametrics(productItem);
            var variantParametricIds = itemModel.Variants.SelectMany(v => v.VariantParametrics.Select(p => p.Id));
            var parametricIdsOnVariants = itemModel.Variants.SelectMany(v => v.Parametrics.Select(p => p.Id));
            allParametrics.RemoveAll(i => variantParametricIds.Contains(i.Id) || parametricIdsOnVariants.Contains(i.Id));
            MapParametrics(itemModel.Parametrics, allParametrics);

            var activeVariants = variants.Where(i => i.StatusId != 5).ToList();
            itemModel.Price = MapMinPrice(activeVariants);
            itemModel.Price.IsFromPrice = activeVariants.Any() && activeVariants.Max(v => v.Price?.Value) != activeVariants.Min(v => v.Price?.Value);

            itemModel.OnHandStatus = itemModel.Variants.Min(x => x.OnHandStatus);

            return itemModel;
        }


        private static PriceModel MapMinPrice(IList<ProductItem> productItemGroup)
        {            
            var productItem = productItemGroup.Any() ? productItemGroup.FirstOrDefault(g => g.Price == productItemGroup.Min(v => v.Price)) : null;
            return MapPrice(productItem);
        }

        private static PriceModel MapMinPrice(List<VariantItem> variants)
        {            
            var variant = variants.Any() ? variants.FirstOrDefault(g => g.Price?.Value == variants.Min(v => v.Price.Value)) : null;
            return MapPrice(variant?.Price);
        }

        private void MapVariants(IEnumerable<ProductItem> variants, ProductItemModel itemModel)
        {
            foreach (var variant in variants)
            {
                var variantModel = Mapper.Map<ProductItem, VariantModel>(variant);
                //variantModel.Files.RemoveAll(f => itemModel.Files.Select(x => x.Url).Contains(f.Url) && f.Type != (int)ProductFileType.VariantImage);

                MapVariantParametrics(variant, variantModel.VariantParametrics);
                MapParametrics(variant, variantModel.Parametrics);
                
                itemModel.Variants.Add(variantModel);
            }

            itemModel.Files.RemoveAll(f => f.Type != (int)ProductFileType.DefaultImage && !itemModel.Variants.All(v => v.Files.Exists(vf => vf.Url == f.Url)));
            foreach (var variantModel in itemModel.Variants)
            {
               variantModel.Files.RemoveAll(f => itemModel.Files.Select(x => x.Url).Contains(f.Url) && f.Type != (int)ProductFileType.VariantImage);
            }
        }

        private void MapVariants(ProductItem productItem, IEnumerable<VariantItem> variants, ProductItemModel itemModel)
        {
            foreach (var variant in variants)
            {
                var variantModel = Mapper.Map<VariantItem, VariantModel>(variant);

                MapVariantParametrics(productItem, variant, variantModel.VariantParametrics);
                MapParametrics(productItem, variant, variantModel.Parametrics);

                itemModel.Variants.Add(variantModel);
            }
        }
        private void MapProductParametrics(ProductItemModel itemModel, ProductItem productItem)
        {
            MoveEqualParametricsFromVariantToProduct(itemModel);

            var variantParametricIds = GetVariantParametricIds(productItem.VariantParametricSeed);
            var parametricsIdsOnVariant = itemModel.Variants.SelectMany(v => v.Parametrics.Select(p => p.Id));
            var alreadyAddedIds = itemModel.Parametrics.Select(p => p.Id);
            var allParametrics = GetAllParametrics(productItem);
            allParametrics.RemoveAll(i => variantParametricIds.Contains(i.Id) || parametricsIdsOnVariant.Contains(i.Id) || alreadyAddedIds.Contains(i.Id));

            MapParametrics(itemModel.Parametrics, allParametrics);
        }

        private static void MoveEqualParametricsFromVariantToProduct(ProductItemModel itemModel)
        {
            foreach (var group in itemModel.Variants.SelectMany(x => x.Parametrics).ToList().GroupBy(x => x.Id))
            {
                var allVariantsHaveTheParametric = itemModel.Variants.All(x => x.Parametrics.SingleOrDefault(y => y.Id == @group.Key) != null);
                if (!allVariantsHaveTheParametric)
                    continue;

                var distinctValueCount = @group.Distinct(EqualityComparer<ParametricModel>.Default).Count();
                if (distinctValueCount != 1)
                    continue;

                itemModel.Parametrics.Add(@group.First());
                itemModel.Variants.ForEach(x => x.Parametrics = x.Parametrics.Where(y => y.Id != @group.Key).ToList());
            }
            }

        private void MapVariantParametrics(ProductItem productItem, ICollection<ParametricModel> parametrics)
        {
            var variantParametricIds = GetVariantParametricIds(productItem.VariantParametricSeed);
            var allParametrics = GetAllParametrics(productItem);
            allParametrics.RemoveAll(i => !variantParametricIds.Contains(i.Id));

            MapParametrics(parametrics, allParametrics);
        }

        private void MapVariantParametrics(ProductItem productItem, VariantItem variant, ICollection<ParametricModel> parametrics)
        {
            var variantParametricIds = GetVariantParametricIds(productItem.VariantParametricSeed);
            var allParametrics = GetAllParametrics(variant);
            allParametrics.RemoveAll(i => !variantParametricIds.Contains(i.Id));

            MapParametrics(parametrics, allParametrics);
        }

        private void MapParametrics(ProductItem productItem, ICollection<ParametricModel> parametrics)
        {
            var variantParametricIds = GetVariantParametricIds(productItem.VariantParametricSeed);
            var allParametrics = GetAllParametrics(productItem);
            allParametrics.RemoveAll(i => variantParametricIds.Contains(i.Id));

            MapParametrics(parametrics, allParametrics);
        }

        private void MapParametrics(ProductItem productItem, VariantItem variant, ICollection<ParametricModel> parametrics)
        {
            var variantParametricIds = GetVariantParametricIds(productItem.VariantParametricSeed);
            var allParametrics = GetAllParametrics(variant);
            allParametrics.RemoveAll(i => variantParametricIds.Contains(i.Id));

            MapParametrics(parametrics, allParametrics);
        }


        private static List<IdValue> GetAllParametrics(ProductItem productItem)
        {
            var allParametrics = new List<IdValue>();
            AppendParametrics(productItem.ParametricListSeed, allParametrics);
            AppendParametrics(productItem.ParametricMultipleSeed, allParametrics);
            AppendParametrics(productItem.ParametricValueSeed, allParametrics);
            return allParametrics;
        }
        
        private static List<IdValue> GetAllParametrics(VariantItem variant)
        {
            var allParametrics = new List<IdValue>();
            if (variant?.Parametrics == null) return allParametrics;

            AppendParametrics(variant.Parametrics.ListSeed, allParametrics);
            AppendParametrics(variant.Parametrics.MultipleSeed, allParametrics);
            AppendParametrics(variant.Parametrics.ValueSeed, allParametrics);
            return allParametrics;
        }

        private static List<int> GetVariantParametricIds(string parametricSeed)
        {
            var variantParametricIds = new List<int>();
            if (!string.IsNullOrWhiteSpace(parametricSeed) && parametricSeed.Split(',').Any())
            {
                variantParametricIds.AddRange(parametricSeed.Split(',').Select(int.Parse));
            }
            return variantParametricIds;
        }

        private void MapParametrics(ICollection<ParametricModel> parametrics, IEnumerable<IdValue> allParametrics)
        {
            foreach (var p in allParametrics)
            {
                ParametricModel parametricModel;
                var pi = dictionary.ParametricInfo(p.Id, StormContext.CultureCode);
                if (pi == null) continue;
                var existing = parametrics.SingleOrDefault(x => x.Id == pi.Id);
                if (pi.Type == ParametricType.ListValue || pi.Type == ParametricType.MultiValue)
                {
                    var pv = dictionary.ParametricValue(pi.Type, int.Parse(p.Value), StormContext.CultureCode);
                    if (pv == null) continue;
                    var value = new ParametricValueModel
                    {
                        Id = pv.Id,
                        Name = pv.Name,
                        Description = pv.Description,
                        Code = pv.Code,
                        ImageUrl = GetImageUrl(pv.ImageKey),
                        Value = pv.Name,
                        SortOrder = pv.SortOrder
                    };

                    if (existing != null)
                    {
                        if(!existing.Values.Exists(v => v.Id == value.Id)) existing.Values.Add(value);
                    }
                    else
                    {
                        parametricModel = new ParametricModel
                        {
                            Id = pi.Id,
                            Name = pi.Name,
                            Description = pi.Description,
                            Uom = pi.Uom,
                            ValueType = pi.ValueType,
                            Values = new List<ParametricValueModel> { value }
                        };
                        parametrics.Add(parametricModel);
                    }
                }
                else
                {
                    if (existing != null)
                    {
                        if (!existing.Values.Exists(v => v.Value == p.Value)) existing.Values.Add(new ParametricValueModel { Value = p.Value });
                    }
                    else
                    {
                        parametricModel = new ParametricModel
                        {
                            Id = pi.Id,
                            Name = pi.Name,
                            Description = pi.Description,
                            Uom = pi.Uom,
                            ValueType = pi.ValueType,
                            Values = new List<ParametricValueModel> { new ParametricValueModel { Value = p.Value } }
                        };
                        parametrics.Add(parametricModel);
                    }
                }
            }

            parametrics.ForEach(p => p.Values.Sort((p1, p2) => p1.SortOrder.CompareTo(p2.SortOrder)));
        }

        private static void AppendParametrics(string parametrics, List<IdValue> allParametrics)
        {
            if (string.IsNullOrWhiteSpace(parametrics)) return;
            allParametrics.AddRange(parametrics.Split(',').Select(parametric => parametric.Split(':')).Select(p => new IdValue {Id = int.Parse(p[0]), Value = p[1]}));
        }        
    }
}
