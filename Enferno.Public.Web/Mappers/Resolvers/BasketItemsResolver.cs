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
    public class BasketItemsResolver : ValueResolver<BasketModel, BasketItemList>
    {
        protected override BasketItemList ResolveCore(BasketModel source)
        {
            var basketItemList = new BasketItemList();
            source.Items.ForEach(item => basketItemList.Add(Mapper.Map<BasketItem>(item)));
            source.Freights.ForEach(item => basketItemList.Add(Mapper.Map<BasketItem>(item)));
            source.Payments.ForEach(item => basketItemList.Add(Mapper.Map<BasketItem>(item)));

            return basketItemList;
        }
    }

    public class BasketModelItemsResolver : ValueResolver<Basket, IEnumerable<BasketItemModel>>
    {
        protected override IEnumerable<BasketItemModel> ResolveCore(Basket source)
        {
            var items = source.Items.Where(item => item.Type != (int)ProductType.Freight && item.Type != (int)ProductType.Invoice).ToList();

            var basketItemList = items.Select(Mapper.Map<BasketItem, BasketItemModel>).ToList();

            return basketItemList;
        }
    }

    public class BasketModelPaymentsResolver : ValueResolver<Basket, IEnumerable<BasketItemModel>>
    {
        protected override IEnumerable<BasketItemModel> ResolveCore(Basket source)
        {
            var items = source.Items.Where(item => item.Type == (int)ProductType.Invoice).ToList();

            var basketItemList = items.Select(Mapper.Map<BasketItem, BasketItemModel>).ToList();

            return basketItemList;
        }
    }

    public class BasketModelFreightsResolver : ValueResolver<Basket, IEnumerable<BasketItemModel>>
    {
        protected override IEnumerable<BasketItemModel> ResolveCore(Basket source)
        {
            var items = source.Items.Where(item => item.Type == (int)ProductType.Freight).ToList();

            var basketItemList = items.Select(Mapper.Map<BasketItem, BasketItemModel>).ToList();

            return basketItemList;
        }
    }
}
