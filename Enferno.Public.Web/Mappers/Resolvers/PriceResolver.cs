using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class ProductPriceResolver : ValueResolver<Product, PriceModel>
    {
        protected override PriceModel ResolveCore(Product product)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(product.Price, product.VatRate),
                Catalog = PriceCalulator.Price(product.PriceCatalog ?? product.Price, product.VatRate),
                Recommended = PriceCalulator.Price(product.PriceRecommended ?? product.Price, product.VatRate),
                Original = PriceCalulator.Price(product.Price, product.VatRate),
                VatRate = product.VatRate,
                PricelistId = product.PriceListId,
                IsFromPrice = false
            };
        }
    }

    public class ProductItemPriceResolver : ValueResolver<ProductItem, PriceModel>
    {
        protected override PriceModel ResolveCore(ProductItem productItem)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(productItem.Price, productItem.VatRate),
                Catalog = PriceCalulator.Price(productItem.PriceCatalog ?? productItem.Price, productItem.VatRate),
                Recommended = PriceCalulator.Price(productItem.PriceRecommended ?? productItem.Price, productItem.VatRate),
                Original = PriceCalulator.Price(productItem.Price, productItem.VatRate),
                VatRate = productItem.VatRate,
                PricelistId = productItem.PriceListId.GetValueOrDefault(0),
                IsFromPrice = false
            };
        }
    }

    public class VariantItemPriceResolver : ValueResolver<VariantItem, PriceModel>
    {
        protected override PriceModel ResolveCore(VariantItem variantItem)
        {
            if(variantItem.Price == null) return new PriceModel();

            var price = variantItem.Price;
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

    public class BasketPriceResolver : ValueResolver<BasketItem, PriceModel>
    {
        protected override PriceModel ResolveCore(BasketItem basketItem)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(basketItem.PriceDisplay, basketItem.VatRate),
                Catalog = PriceCalulator.Price(basketItem.PriceCatalog ?? basketItem.PriceOriginal, basketItem.VatRate),
                Recommended = PriceCalulator.Price(basketItem.PriceRecommended ?? basketItem.PriceOriginal, basketItem.VatRate),
                Original = PriceCalulator.Price(basketItem.PriceOriginal, basketItem.VatRate),
                VatRate = basketItem.VatRate,
                PricelistId = basketItem.PriceListId,
                IsFromPrice = false
            };
        }
    }
}
