using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class PromotionsResolver : IValueResolver<BasketModel, Basket, PromotionList>
    {
        public PromotionList Resolve(BasketModel source, Basket destination, PromotionList destMember, ResolutionContext context)
        {
            var promotionList = new PromotionList();
            promotionList.AddRange(source.Promotions.Select(Mapper.Map<PromotionModel, Promotion>));

            return promotionList;
        }
    }

    public class PromotionModelsResolver : IValueResolver<Basket, BasketModel, List<PromotionModel>>
    {
        public List<PromotionModel> Resolve(Basket source, BasketModel destination, List<PromotionModel> destMember, ResolutionContext context)
        {
            var promotionModelList = source.AppliedPromotions.Select(Mapper.Map<Promotion, PromotionModel>).ToList();
            return promotionModelList;
        }
    }
}
