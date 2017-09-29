
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Builders
{
    public class BasketBuilder : BuilderBase
    {
        public BasketBuilder()
        {
        }

        public BasketBuilder(ISiteRules rules) : base(rules)
        {
        }

        public BasketModel MapBasketModel(Basket basket)
        {
            return Mapper.Map<BasketModel>(basket);
        }

        public Basket MapBasket(BasketModel basketModel)
        {
            return Mapper.Map<Basket>(basketModel);
        }

        public BasketItemModel MapBasketItemModel(BasketItem basketItem)
        {
            return Mapper.Map<BasketItemModel>(basketItem);
        }

        public BasketItem MapBasketItem(BasketItemModel basketItemModel)
        {
            return Mapper.Map<BasketItem>(basketItemModel);
        }
    }
}
