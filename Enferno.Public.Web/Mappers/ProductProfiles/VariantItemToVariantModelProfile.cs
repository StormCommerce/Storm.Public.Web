﻿using AutoMapper;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.ProductProfiles
{
    public class VariantItemToVariantModelProfile : Profile
    {
        public VariantItemToVariantModelProfile()
        {
            CreateMap<VariantItem, VariantModel>()
                .ForMember(to => to.ManufacturePartNo, opts => opts.MapFrom(from => from.ManufacturerPartNo))
                .ForMember(to => to.Price, opts => opts.ResolveUsing<VariantItemPriceResolver>())
                .ForMember(to => to.Flags, opts => opts.ResolveUsing<VariantItemVariantFlagsResolver>())
                .ForMember(to => to.Files, opts => opts.ResolveUsing<VariantItemVariantFilesResolver>())
                .ForMember(to => to.ImageKey, opts => opts.MapFrom(from => from.ImageKey.ToString()))
                .ForMember(to => to.ImageUrl,
                    opts =>
                        opts.ResolveUsing(
                            from => from.ImageKey.HasValue ? Link.ImageUrl(from.ImageKey.ToString()) : null))
                .ForMember(to => to.OnHandStatus, opts => opts.ResolveUsing<VariantItemOnHandStatusResolver>())
                .ForMember(to => to.VariantParametrics, opts => opts.Ignore()) //mapped in the builder
                .ForMember(to => to.Parametrics, opts => opts.Ignore());
        }
    }
}
