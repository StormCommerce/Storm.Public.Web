using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers
{
    public class BasketToBasketModelProfile : Profile
    {
        protected override void Configure()
        {
            var siteRules = IoC.Resolve<ISiteRules>();
            Mapper.CreateMap<Basket, BasketModel>()
                .ForMember(basketModel => basketModel.Promotions,
                    basketConfig => basketConfig.MapFrom(basket => basket.AppliedPromotions))
                .ForMember(basketModel => basketModel.OnHand,
                    basketConfig => basketConfig.MapFrom(basket => siteRules.GetOnHandStatus(basket)))
                .ForMember(basketModel => basketModel.Freight,
                    basketConfig => basketConfig.Ignore());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
