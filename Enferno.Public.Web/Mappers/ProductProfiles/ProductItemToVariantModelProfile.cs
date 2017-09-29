using AutoMapper;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class ProductItemToVariantModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProductItem, VariantModel>()
                .ForMember(to => to.ManufacturePartNo, opts => opts.MapFrom(from => from.Manufacturer.PartNo))
                .ForMember(to => to.Name, opts => opts.MapFrom(from => from.VariantName))
                .ForMember(to => to.Price, opts => opts.ResolveUsing<ProductItemPriceResolver>())
                .ForMember(to => to.Flags, opts => opts.ResolveUsing<ProductItemVariantFlagsResolver>())
                .ForMember(to => to.Files, opts => opts.ResolveUsing<VariantItemFilesResolver>())
                .ForMember(to => to.ImageKey, opts => opts.MapFrom(from => from.VariantImageKey.ToString()))
                .ForMember(to => to.ImageUrl,
                    opts =>
                        opts.ResolveUsing(
                            from => from.VariantImageKey.HasValue ? Link.ImageUrl(from.VariantImageKey.ToString()) : null))
                .ForMember(to => to.OnHandStatus, opts => opts.ResolveUsing<ProductItemOnHandStatusResolver>())
                .ForMember(to => to.VariantParametrics, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.Parametrics, opts => opts.Ignore());
        }
    }
}
