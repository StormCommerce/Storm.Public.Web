using AutoMapper;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.BasketProfiles
{
    public class BasketModelToBasketProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BasketModel, Basket>()
                .ForMember(basket => basket.SalesContactId, expr => expr.Ignore())
                .ForMember(basket => basket.StatusId, expr => expr.Ignore())
                .ForMember(basket => basket.CurrencyId, expr => expr.Ignore())
                .ForMember(basket => basket.CurrencyCode, expr => expr.Ignore())
                .ForMember(basket => basket.ReferId, expr => expr.Ignore())
                .ForMember(basket => basket.ReferUrl, expr => expr.Ignore())
                .ForMember(basket => basket.ValidTo, expr => expr.Ignore())
                .ForMember(basket => basket.IsEditable, expr => expr.Ignore())
                .ForMember(basket => basket.Info, expr => expr.Ignore())
                .ForMember(basket => basket.Summary, expr => expr.Ignore())
                .ForMember(basket => basket.ExtensionData, expr => expr.Ignore())
                .ForMember(basket => basket.IpAddress, expr => expr.Ignore())
                .ForMember(basket => basket.AttestedBy, expr => expr.Ignore())
                .ForMember(basket => basket.TypeId, opts => opts.Ignore())

                .ForMember(basket => basket.AppliedPromotions, expr => expr.ResolveUsing<PromotionsResolver>())
                .ForMember(basket => basket.Items, expr => expr.ResolveUsing<BasketItemsResolver>());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }


}
