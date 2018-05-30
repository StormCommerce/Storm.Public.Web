using System;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class ProductPriceResolver<TDest> : IValueResolver<Product, TDest, PriceModel>
    {
        public PriceModel Resolve(Product source, TDest destination, PriceModel destMember, ResolutionContext context)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(source.Price, source.VatRate),
                Catalog = PriceCalulator.Price(source.PriceCatalog ?? source.Price, source.VatRate),
                Recommended = PriceCalulator.Price(source.PriceRecommended ?? source.Price, source.VatRate),
                Original = PriceCalulator.Price(source.Price, source.VatRate),
                VatRate = source.VatRate,
                PricelistId = source.PriceListId,
                IsFromPrice = false
            };
        }
    }

    public class ProductItemPriceResolver<TDest> : IValueResolver<ProductItem, TDest, PriceModel>
    {
        public PriceModel Resolve(ProductItem source, TDest destination, PriceModel destMember, ResolutionContext context)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(source.Price, source.VatRate),
                Catalog = PriceCalulator.Price(source.PriceCatalog ?? source.Price, source.VatRate),
                Recommended = PriceCalulator.Price(source.PriceRecommended ?? source.Price, source.VatRate),
                Original = PriceCalulator.Price(source.Price, source.VatRate),
                VatRate = source.VatRate,
                PricelistId = source.PriceListId.GetValueOrDefault(0),
                IsFromPrice = false
            };
        }
    }

    public class VariantItemPriceResolver : IValueResolver<VariantItem, VariantModel, PriceModel>
    {
        public PriceModel Resolve(VariantItem source, VariantModel destination, PriceModel destMember, ResolutionContext context)
        {
            if (source.Price == null) return new PriceModel();

            var price = source.Price;
            return new PriceModel
            {
                Display = PriceCalulator.Price(price.Value, price.VatRate),
                Catalog = PriceCalulator.Price(price.Catalog ?? price.Value, price.VatRate),
                Recommended = PriceCalulator.Price(price.Recommended ?? price.Value, price.VatRate),
                Original = PriceCalulator.Price(price.Value, price.VatRate),
                VatRate = price.VatRate,
                PricelistId = price.PriceListId,
                IsFromPrice = false
            };
        }
    }

    public class BasketPriceResolver : IValueResolver<BasketItem, BasketItemModel, PriceModel>
    {
        public PriceModel Resolve(BasketItem source, BasketItemModel destination, PriceModel destMember, ResolutionContext context)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(source.PriceDisplay, source.VatRate),
                Catalog = PriceCalulator.Price(source.PriceCatalog ?? source.PriceOriginal, source.VatRate),
                Recommended = PriceCalulator.Price(source.PriceRecommended ?? source.PriceOriginal, source.VatRate),
                Original = PriceCalulator.Price(source.PriceOriginal, source.VatRate),
                VatRate = source.VatRate,
                PricelistId = source.PriceListId,
                IsFromPrice = false
            };
        }
    }
}
