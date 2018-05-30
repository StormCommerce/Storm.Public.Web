
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductItemToProductItemModelProfile : Profile
    {
        public ProductItemToProductItemModelProfile()
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            CreateMap<ProductItem, ProductItemModel>()
                .ForMember(to => to.Url, opts => opts.ResolveUsing(siteRules.GetProductPageUrl))
                .ForMember(to => to.Price, opts => opts.Ignore()) //is mapped in the builder
                .ForMember(to => to.Flags, opts => opts.ResolveUsing<ProductItemFlagsResolver>())
                .ForMember(to => to.Files, opts => opts.ResolveUsing<ProductItemFilesResolver>())
                .ForMember(to => to.OnHandStatus, opts => opts.ResolveUsing<ProductItemOnHandStatusResolver<ProductItemModel>>())
                .ForMember(to => to.Variants, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.Parametrics, opts => opts.Ignore());
        }
    }
}
