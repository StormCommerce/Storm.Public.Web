
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

using ParametricValue = Enferno.StormApiClient.Products.ParametricValue;

namespace Enferno.Public.Web.Builders
{

    public interface IBuilder<TEntity, TViewModel>
    {
        TViewModel BuildModel(TEntity entity);
    }

    public class ProductBuilder : BuilderBase //IBuilder<Product, ProductViewModel>
    {
        private ProductModel model;

        private readonly IApplicationDictionary dictionary;

        public ProductBuilder() : this(ApplicationDictionary.Instance)
        {
            
        }

        public ProductBuilder(IApplicationDictionary dictionary, ISiteRules rules = null) : base(rules)
        {
            this.dictionary = dictionary;
        }

        public ProductBuilder(ISiteRules rules) : base(rules)
        {
            dictionary = ApplicationDictionary.Instance;
        }

        //public ProductModel BuildModel(Product prodcut)
        public ProductModel BuildProductModel(Product product)
        {
            return product == null ? null : MapProductModel(product);
        }

        private ProductModel MapProductModel(Product product)
        {
            model = Mapper.Map<Product, ProductModel>(product);
            MapVariants(product);
            MapParametrics(product, model.Parametrics);
            MapFamilies(product);

            return model;
        }
        
        private void MapVariants(Product product)
        {
            if (product.Variants == null || product.Variants.Count == 0)
            {
                model.Variants.Add(Mapper.Map<Product, VariantModel>(product));               
                model.SelectedVariant = model.Variants[0];
            }
            else
            {
                model.Price.IsFromPrice = product.Variants.Max(v => v.Price) != product.Variants.Min(v => v.Price);

                foreach (var variant in product.Variants)
                {
                    var variantModel = Mapper.Map<Product, VariantModel>(variant);
                    MapVariantParametrics(variant, variantModel);
                    MapParametrics(variant, variantModel.Parametrics);
                    model.Variants.Add(variantModel);
                }
            }
        }

        private void MapVariantParametrics(Product variant, VariantModel modelVariant)
        {
            if (variant.VariantParametrics == null || variant.VariantParametrics.Count <= 0) return;
            foreach (var parametric in variant.VariantParametrics)
            {
                var parametricModel = MapParametric(parametric);
                if (parametricModel == null) continue;

                modelVariant.VariantParametrics.Add(parametricModel);

                var productModelParametric = model.VariantParametrics.Find(v => v.Id == parametricModel.Id);
                if (productModelParametric == null)
                {
                    productModelParametric = MapParametric(parametric);
                    model.VariantParametrics.Add(productModelParametric);
                }

                //add value to the product model level parametric if the parametric does not already contain that value
                foreach (var parametricValue in parametricModel.Values)
                {
                    //the value has an id, i.e it is of list type. Use that for uniqueness.
                    if (parametric.ValueId.HasValue &&
                        !productModelParametric.Values.Exists(value => value.Id == parametricValue.Id))
                    {
                        productModelParametric.Values.Add(parametricValue);
                    }
                    else if (!productModelParametric.Values.Exists(value => value.Value == parametricValue.Value)) //we don't have an id, use value for uniqueness
                    {
                        productModelParametric.Values.Add(parametricValue);
                    }    
                }

                productModelParametric.Values.Sort((p1, p2) => p1.SortOrder.CompareTo(p2.SortOrder));
            }
        }

        private void MapParametrics(Product product, List<ParametricModel> parametrics)
        {
            if (product.Parametrics == null || product.Parametrics.Count <= 0) return;
            parametrics.AddRange(product.Parametrics.Select(MapParametric).Where(p => p != null));
            parametrics.ForEach(p => p.Values.Sort((p1, p2) => p1.SortOrder.CompareTo(p2.SortOrder)));
        }              

        private void MapFamilies(Product product)
        {
            if (product.Families == null || product.Families.Count <= 0) return;
            foreach (var family in product.Families)
            {
                model.Families.Add(new ProductFamilyModel
                {
                    Description = family.Description,
                    Id = family.Id,
                    Name = family.Name,
                    ImageUrl = GetImageUrl(family.ImageKey),
                });
            }
        }

        private ParametricModel MapParametric(ProductParametric p)
        {
            var parametricInfo = dictionary.ParametricInfo(p.Id, StormContext.CultureCode);

            if (parametricInfo.Type == ParametricType.ListValue)
            {
                if (p.ValueId == null) return null;

                var parametric = MapProductParametric(p, parametricInfo);                
                var parametricValue = dictionary.ParametricValue(parametricInfo.Type, p.ValueId.Value, StormContext.CultureCode);
                if (parametricValue == null) return parametric;
                var value = MapProductParametricValue(parametricValue);                
                parametric.Values.Add(value);
                return parametric;
            }
            
            if (parametricInfo.Type == ParametricType.MultiValue)
            {
                if (string.IsNullOrWhiteSpace(p.ValueIdSeed)) return null;
                var parametric = MapProductParametric(p, parametricInfo);
                foreach (var v in p.ValueIdSeed.Split(','))
                {
                    int id;
                    if(!int.TryParse(v, out id)) return null;
                    var parametricValue = dictionary.ParametricValue(parametricInfo.Type, id, StormContext.CultureCode);
                    if (parametricValue == null) continue;
                    var value = MapProductParametricValue(parametricValue);
                    parametric.Values.Add(value);
                }
                return parametric;
            }
            
            if (p.ValueId.HasValue)
            {
                var value = new ParametricValueModel { Id = p.ValueId.Value, Value = p.Value2 ?? "", };
                var parametric = MapProductParametric(p, parametricInfo);
                parametric.Values.Add(value);
                return parametric;
            }            
            else
            {
                var value = new ParametricValueModel { Value = p.Value2 ?? "" };
                var parametric = MapProductParametric(p, parametricInfo);
                parametric.Values.Add(value);
                return parametric;
            }
        }

        private static ParametricValueModel MapProductParametricValue(ParametricValue parametricValue)
        {
            var value = new ParametricValueModel
            {
                Id = parametricValue.Id,
                Value = parametricValue.Name,
                Code = parametricValue.Code,
                Description = parametricValue.Description,
                ImageUrl = GetImageUrl(parametricValue.ImageKey),
                Name = parametricValue.Name,
                SortOrder = parametricValue.SortOrder,  
                
            };
            return value;
        }

        private static ParametricModel MapProductParametric(ProductParametric p, ParametricInfo pi)
        {
            var parametric = new ParametricModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Uom = p.Uom,
                IsPrimary = p.IsPrimary,
                GroupId = p.GroupId,
                Group = p.GroupName,
                ValueType = pi.ValueType,
                Values = new List<ParametricValueModel>()
            };
            return parametric;
        }
    }
}
