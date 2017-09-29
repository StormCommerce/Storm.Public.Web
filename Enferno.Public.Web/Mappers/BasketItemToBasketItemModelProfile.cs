using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers
{
    public class BasketItemToBasketItemModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BasketItem, BasketItemModel>()
                //TODO: how to build image urls
                .ForMember(basketItemModel => basketItemModel.ImageUrl,
                    basketItemMapConfig => basketItemMapConfig.MapFrom(basketItem => basketItem.ImageKey))
                .ForMember(basketItemModel => basketItemModel.Price,
                    basketMapConfig => basketMapConfig.ResolveUsing<BasketPriceResolver>())
                .ForMember(basketItemModel => basketItemModel.ProductUrl,
                    basketItemMapConfig => basketItemMapConfig.Ignore());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

    public class BasketPriceResolver : ValueResolver<BasketItem, PriceModel>
    {
        protected override PriceModel ResolveCore(BasketItem basketItem)
        {
            return new PriceModel
            {
                Display = PriceCalulator.Price(basketItem.Price, basketItem.VatRate),
                Catalog = PriceCalulator.Price(basketItem.PriceCatalog ?? basketItem.Price, basketItem.VatRate),
                Recommended = PriceCalulator.Price(basketItem.PriceRecommended ?? basketItem.Price, basketItem.VatRate),
                VatRate = basketItem.VatRate,
                PricelistId = basketItem.PriceListId,
                IsFromPrice = false
            };
        }
    }
}