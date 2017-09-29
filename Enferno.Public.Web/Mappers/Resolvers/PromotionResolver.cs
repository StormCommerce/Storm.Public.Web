using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class PromotionsResolver : ValueResolver<BasketModel, PromotionList>
    {
        protected override PromotionList ResolveCore(BasketModel source)
        {
            var promotionList = new PromotionList();
            promotionList.AddRange(source.Promotions.Select(Mapper.Map<PromotionModel, Promotion>));

            return promotionList;
        }
    }

    public class PromotionModelsResolver : ValueResolver<Basket, IEnumerable<PromotionModel>>
    {
        protected override IEnumerable<PromotionModel> ResolveCore(Basket source)
        {
            var promotionModelList = source.AppliedPromotions.Select(Mapper.Map<Promotion, PromotionModel>).ToList();
            return promotionModelList;
        }
    }
}
