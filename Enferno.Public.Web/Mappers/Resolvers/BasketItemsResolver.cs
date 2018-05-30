using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.Resolvers
{
    public class BasketItemsResolver : IValueResolver<BasketModel, Basket, BasketItemList>
    {
        public BasketItemList Resolve(BasketModel source, Basket destination, BasketItemList destMember, ResolutionContext context)
        {
            var basketItemList = new BasketItemList();
            source.Items.ForEach(item => basketItemList.Add(Mapper.Map<BasketItem>(item)));
            source.Freights.ForEach(item => basketItemList.Add(Mapper.Map<BasketItem>(item)));
            source.Payments.ForEach(item => basketItemList.Add(Mapper.Map<BasketItem>(item)));

            return basketItemList;
        }
    }

    public class BasketModelItemsResolver : IValueResolver<Basket, BasketModel, List<BasketItemModel>>
    {
        public List<BasketItemModel> Resolve(Basket source, BasketModel destination, List<BasketItemModel> destMember, ResolutionContext context)
        {
            var items = source.Items.Where(item => item.Type != (int)ProductType.Freight && item.Type != (int)ProductType.Invoice).ToList();

            var basketItemList = items.Select(Mapper.Map<BasketItem, BasketItemModel>).ToList();

            return basketItemList;
        }
    }

    public class BasketModelPaymentsResolver : IValueResolver<Basket, BasketModel, List<BasketItemModel>>
    {
        public List<BasketItemModel> Resolve(Basket source, BasketModel destination, List<BasketItemModel> destMember, ResolutionContext context)
        {
            var items = source.Items.Where(item => item.Type == (int)ProductType.Invoice).ToList();

            var basketItemList = items.Select(Mapper.Map<BasketItem, BasketItemModel>).ToList();

            return basketItemList;
        }
    }

    public class BasketModelFreightsResolver : IValueResolver<Basket, BasketModel, List<BasketItemModel>>
    {
        public List<BasketItemModel> Resolve(Basket source, BasketModel destination, List<BasketItemModel> destMember, ResolutionContext context)
        {
            var items = source.Items.Where(item => item.Type == (int)ProductType.Freight).ToList();

            var basketItemList = items.Select(Mapper.Map<BasketItem, BasketItemModel>).ToList();

            return basketItemList;
        }
    }
}
