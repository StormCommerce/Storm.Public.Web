using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class VariantItemOnHandStatusResolver : ValueResolver<VariantItem, OnHandStatusModel>
    {
        protected override OnHandStatusModel ResolveCore(VariantItem variantItem)
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            return siteRules.GetOnHandStatus(variantItem);
        }
    }
    public class ProductItemOnHandStatusResolver : ValueResolver<ProductItem, OnHandStatusModel>
    {
        protected override OnHandStatusModel ResolveCore(ProductItem productItem)
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            return siteRules.GetOnHandStatus(productItem);
        }
    }

    public class ProductOnHandStatusResolver : ValueResolver<Product, OnHandStatusModel>
    {
        protected override OnHandStatusModel ResolveCore(Product product)
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            return siteRules.GetOnHandStatus(product);
        }
    }

    public class BasketOnHandStatusResolver : ValueResolver<Basket, OnHandStatusModel>
    {
        protected override OnHandStatusModel ResolveCore(Basket basket)
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            return siteRules.GetOnHandStatus(basket);
        }
    }

    public class WarehouseOnHandStatusResolver : ValueResolver<Warehouse, OnHandStatusModel>
    {
        protected override OnHandStatusModel ResolveCore(Warehouse warehouse)
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            return siteRules.GetOnHandStatus(warehouse);
        }
    }
}
