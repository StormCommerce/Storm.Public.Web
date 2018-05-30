
using System;
using System.Collections.Generic;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;
using Unity;


namespace Enferno.Public.Web.Builders
{
    public abstract class BuilderBase
    {
        protected readonly ISiteRules SiteRules;

        protected BuilderBase()
        {
            SiteRules = IoC.Container.Resolve<ISiteRules>();
        }
        protected BuilderBase(ISiteRules siteRules)
        {
            SiteRules = siteRules ?? IoC.Container.Resolve<ISiteRules>();
        }

        protected static string GetImageUrl(Guid? imageKey)
        {
            return imageKey.HasValue ? Link.ImageUrl(imageKey.ToString()) : null;
        }

        protected static ProductManufacturerModel MapProductManufacturer(ProductManufacturer manufacturer)
        {
            return new ProductManufacturerModel
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                ImageUrl = GetImageUrl(manufacturer.LogoKey)
            };
        }

        protected static void MapFlags(string flagSeed, ICollection<int> flagList)
        {
            if (string.IsNullOrWhiteSpace(flagSeed)) return;
            foreach (var flag in flagSeed.Split(','))
            {
                int id;
                if (int.TryParse(flag.Trim(), out id) && ! flagList.Contains(id)) flagList.Add(id);
            }
        }

        // TODO : Duplicate code                
        protected static PriceModel MapPrice(Product product)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(product.Price, product.VatRate),
                Catalog = PriceCalulator.Price(product.PriceCatalog ?? product.Price, product.VatRate),
                Recommended = PriceCalulator.Price(product.PriceRecommended ?? product.Price, product.VatRate),
                VatRate = product.VatRate,
                PricelistId = product.PriceListId,
                IsFromPrice = false
            };
        }

        protected static PriceModel MapPrice(ProductItem product)
        {
            if (product == null) return new PriceModel();

            return new PriceModel
            {
                Display = PriceCalulator.Price(product.Price, product.VatRate),
                Catalog = PriceCalulator.Price(product.PriceCatalog ?? product.Price, product.VatRate),
                Recommended = PriceCalulator.Price(product.PriceRecommended ?? product.Price, product.VatRate),
                VatRate = product.VatRate,
                PricelistId = product.PriceListId.GetValueOrDefault(0),
                IsFromPrice = false
            };
        }

        protected static PriceModel MapPrice(ProductPrice price)
        {
            if(price == null) return new PriceModel();

            return new PriceModel
            {
                Display = PriceCalulator.Price(price.Value, price.VatRate),
                Catalog = PriceCalulator.Price(price.Catalog ?? price.Value, price.VatRate),
                Recommended = PriceCalulator.Price(price.Recommended ?? price.Value, price.VatRate),
                VatRate = price.VatRate,
                PricelistId = price.PriceListId,
                IsFromPrice = false
            };
        }
    }
}
