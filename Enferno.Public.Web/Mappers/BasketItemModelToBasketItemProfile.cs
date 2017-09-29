using System;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers
{
    public class BasketItemModelToBasketItemProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BasketItemModel, BasketItem>()
                .ForMember(basketItem => basketItem.ParentLineNo,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ProductId,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ManufacturerPartNo,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ThumbnailImage,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.FlagIdSeed,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.PriceOriginal,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.Cost,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.UOM,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.Comment,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ReferId,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ReferUrl,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.IsDiscountable,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.Info,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.OptionalItems,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.OnHandValue,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.IncomingValue,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.NextDeliveryDate,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.LeadtimeDayCount,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.PromotionIdSeed,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ManufacturerName,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.CategoryId,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.OnHand,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.OnHandSupplier,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ManufacturerId,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.UniqueName,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.StatusId,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.StockDisplayBreakPoint,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.IsBuyable,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.ExtensionData,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())
                .ForMember(basketItem => basketItem.Type,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.Ignore())

                        //TODO: how to get key from url
                .ForMember(basketItem => basketItem.ImageKey,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.MapFrom(basketItemModel => Guid.NewGuid() /*basketItemModel.ImageUrl*/))
                .ForMember(basketItem => basketItem.Price,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.MapFrom(basketItemModel => basketItemModel.Price.Display))
                .ForMember(basketItem => basketItem.PriceCatalog,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.MapFrom(basketItemModel => basketItemModel.Price.Catalog))
                .ForMember(basketItem => basketItem.PriceRecommended,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.MapFrom(basketItemModel => basketItemModel.Price.Recommended))
                .ForMember(basketItem => basketItem.VatRate,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.MapFrom(basketItemModel => basketItemModel.Price.VatRate))
                .ForMember(basketItem => basketItem.PriceListId,
                    basketItemModelMapConfig =>
                        basketItemModelMapConfig.MapFrom(basketItemModel => basketItemModel.Price.PricelistId));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }
    }
}