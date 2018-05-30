using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Mappers.Resolvers;
using Enferno.Public.Web.Models;

namespace Enferno.Public.Web.Mappers.BasketProfiles
{
    public class BasketToBasketModelProfile : Profile
    {
        public BasketToBasketModelProfile()
        {
            //var siteRules = IoC.Resolve<ISiteRules>();
            var siteRules = IoC.Resolve<ISiteRules>();
            CreateMap<StormApiClient.Shopping.Basket, BasketModel>()               
                .ForMember(basketModel => basketModel.Promotions,
                    basketConfig => basketConfig.ResolveUsing<PromotionModelsResolver>())
                .ForMember(basketModel => basketModel.OnHand,
                    basketConfig => basketConfig.MapFrom(basket => siteRules.GetOnHandStatus(basket)))
                .ForMember(to => to.Freights,
                    opts => opts.ResolveUsing<BasketModelFreightsResolver>())
                .ForMember(to => to.Payments,
                    opts => opts.ResolveUsing<BasketModelPaymentsResolver>())
                .ForMember(to => to.Items,
                    opts => opts.ResolveUsing<BasketModelItemsResolver>());
        }

        public override string ProfileName => GetType().Name;
    }
}
