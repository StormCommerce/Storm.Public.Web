using System;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.BasketProfiles
{
    public class BasketItemToBasketItemModelProfile : Profile
    {
        protected override void Configure()
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            Mapper.CreateMap<BasketItem, BasketItemModel>()
                .ForMember(to => to.ImageUrl,
                    opts => opts.MapFrom(from => from.ImageKey.HasValue ? Link.ImageUrl(from.ImageKey.ToString()) : null))
                .ForMember(to => to.Price,
                    opts => opts.ResolveUsing<BasketPriceResolver>())
                .ForMember(to=> to.UnitOfMeasurement, opts=> opts.MapFrom(from=> from.UOM))
                //.ForMember(to => to.Type, opts=> opts.MapFrom(from => from.Type))
                //.ForMember(to => to.IsEditable, opts => opts.MapFrom(from => from.IsEditable))
                //.ForMember(to => to.IsDiscountable, opts => opts.MapFrom(from => from.IsDiscountable))
                .ForMember(to => to.ProductUrl,
                    opts => opts.MapFrom(from=> siteRules.GetProductPageUrl(from)));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}