using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Mappers.ProductProfiles;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class VariantItemOnHandStatusResolver : IValueResolver<VariantItem, VariantModel, OnHandStatusModel>
    {
        public OnHandStatusModel Resolve(VariantItem source, VariantModel destination, OnHandStatusModel destMember, ResolutionContext context)
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            return siteRules.GetOnHandStatus(source);
        }
    }

    public class ProductItemOnHandStatusResolver<TDest>: IValueResolver<ProductItem, TDest, OnHandStatusModel>
    {
        public OnHandStatusModel Resolve(ProductItem source, TDest destination, OnHandStatusModel destMember, ResolutionContext context)
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            return siteRules.GetOnHandStatus(source);
        }
    }

    public class ProductOnHandStatusResolver<TDest> : IValueResolver<Product, TDest, OnHandStatusModel>
    {
        public OnHandStatusModel Resolve(Product source, TDest destination, OnHandStatusModel destMember, ResolutionContext context)
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            return siteRules.GetOnHandStatus(source);
        }
    }

    public class BasketOnHandStatusResolver : IValueResolver<Basket, BasketModel, OnHandStatusModel>
    {
        public OnHandStatusModel Resolve(Basket source, BasketModel destination, OnHandStatusModel destMember, ResolutionContext context)
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            return siteRules.GetOnHandStatus(source);
        }
    }

    public class WarehouseOnHandStatusResolver : IValueResolver<Warehouse, WarehouseModel, OnHandStatusModel>
    {
        public OnHandStatusModel Resolve(Warehouse source, WarehouseModel destination, OnHandStatusModel destMember, ResolutionContext context)
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            return siteRules.GetOnHandStatus(source);
        }
    }
}
