using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers
{
    public class BasketModelToBasketProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BasketModel, Basket>()
                .ForMember(basket => basket.SalesContactId,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.StatusId,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.CurrencyId,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.CurrencyCode,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.Comment,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.OrderReference,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.ReferId,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.ReferUrl,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.ValidTo,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.IsEditable,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.Info,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.Summary,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.ExtensionData,
                    basketModelMapConfig => basketModelMapConfig.Ignore())
                .ForMember(basket => basket.AppliedPromotions,
                    basketModelMapConfig => basketModelMapConfig.MapFrom(basketModel => basketModel.Promotions))
                .ForMember(basket => basket.Items,
                    basketModelMapConfig => basketModelMapConfig.ResolveUsing<BasketItemListResolver>());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

    public class BasketItemListResolver : ValueResolver<BasketModel, BasketItemList>
    {
        protected override BasketItemList ResolveCore(BasketModel source)
        {
            var basketItemList = new BasketItemList();
            source.Items.ForEach(item=> basketItemList.Add(Mapper.Map<BasketItem>(item)));

            return basketItemList;
        }
    }
}
