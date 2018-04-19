
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.BasketProfiles
{
    public class BasketItemModelToBasketItemProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BasketItemModel, BasketItem>()
                .ForMember(item => item.ParentLineNo, expr => expr.Ignore())
                .ForMember(item => item.ProductId, expr => expr.Ignore())
                .ForMember(item => item.ManufacturerPartNo, expr => expr.Ignore())
                .ForMember(item => item.ThumbnailImage, expr => expr.Ignore())
                .ForMember(item => item.FlagIdSeed, expr => expr.Ignore())
                .ForMember(item => item.Price, expr => expr.Ignore())
                .ForMember(item => item.Cost, expr => expr.Ignore())
                .ForMember(item => item.UOM, expr => expr.Ignore())
                .ForMember(item => item.UOMCount, expr => expr.Ignore())
                .ForMember(item => item.Comment, expr => expr.Ignore())
                .ForMember(item => item.ReferId, expr => expr.Ignore())
                .ForMember(item => item.ReferUrl, expr => expr.Ignore())
                .ForMember(item => item.DescriptionHeader, expr => expr.Ignore())
                .ForMember(item => item.SubDescription, expr => expr.Ignore())
                .ForMember(item => item.IsSubscribable, expr => expr.Ignore())
                .ForMember(item => item.Info, expr => expr.Ignore())
                .ForMember(item => item.OptionalItems, expr => expr.Ignore())
                .ForMember(item => item.OnHandValue, expr => expr.Ignore())
                .ForMember(item => item.IncomingValue, expr => expr.Ignore())
                .ForMember(item => item.NextDeliveryDate, expr => expr.Ignore())
                .ForMember(item => item.LeadtimeDayCount, expr => expr.Ignore())
                .ForMember(item => item.PromotionIdSeed, expr => expr.Ignore())
                .ForMember(item => item.ManufacturerName, expr => expr.Ignore())
                .ForMember(item => item.CategoryId, expr => expr.Ignore())
                .ForMember(item => item.OnHand, expr => expr.Ignore())
                .ForMember(item => item.OnHandSupplier, expr => expr.Ignore())
                .ForMember(item => item.ManufacturerId, expr => expr.Ignore())
                .ForMember(item => item.UniqueName, expr => expr.Ignore())
                .ForMember(item => item.StatusId, expr => expr.Ignore())
                .ForMember(item => item.StockDisplayBreakPoint, expr => expr.Ignore())
                .ForMember(item => item.IsBuyable, expr => expr.Ignore())
                .ForMember(item => item.ExtensionData, expr => expr.Ignore())
                .ForMember(item => item.Type, expr => expr.Ignore())
                .ForMember(item => item.CategoryIdSeed, expr => expr.Ignore())
                .ForMember(item => item.AppliedPromotions, expr => expr.Ignore())
                .ForMember(item => item.RequirementPromotionIdSeed, expr => expr.Ignore())
                .ForMember(item => item.IsPriceManual, expr => expr.Ignore())
                //TODO: how to get key from url
                .ForMember(item => item.ImageKey, expr => expr.Ignore())
                .ForMember(item => item.IsRecommendedQuantityFixed, expr => expr.Ignore())

                .ForMember(item => item.PriceDisplay, expr => expr.MapFrom(model => model.Price.Display))
                .ForMember(item => item.PriceCatalog, expr => expr.MapFrom(model => model.Price.Catalog))
                .ForMember(item => item.PriceRecommended, expr => expr.MapFrom(model => model.Price.Recommended)) 
                .ForMember(item => item.VatRate, expr => expr.MapFrom(model => model.Price.VatRate)) 
                .ForMember(item => item.PriceListId, expr => expr.MapFrom(model => model.Price.PricelistId))
                .ForMember(item => item.PriceOriginal, expr => expr.MapFrom(model => model.Price.Original));
        }

        public override string ProfileName => GetType().Name;
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