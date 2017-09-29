
using System;
using System.Linq;
using System.Xml.Linq;
using Enferno.StormApiClient;
using Enferno.StormApiClient.Customers;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Repositories
{
    public class ShoppingRepository : RepositoryBase, IShoppingRepository
    {
        public ShoppingRepository() : this(null)
        { }

        public ShoppingRepository(IAccessClient client = null)
        {
            MyClient = client;
        }

        public Basket GetBasket(int id, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ShoppingProxy.GetBasket(id, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public ProductOnHandList GetOnHandForBasketProducts(int basketId, Warehouse warehouse, string pricelistSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ShoppingProxy.ListProductOnHandByBasket(basketId, PricelistSeed(pricelistSeed), warehouse, CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public ProductOnHandList GetExternalOnHandForBasketProducts(int basketId, Warehouse warehouse, string pricelistSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.ListExternalProductOnHandByBasket(basketId, PricelistSeed(pricelistSeed), warehouse, CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Basket CreateBasket(string callerIpAddress)
        {
            if (string.IsNullOrWhiteSpace(callerIpAddress))
                throw new ArgumentException("The callers IP must be given", "callerIpAddress");

            using (var client = GetBatch())
            {
                var basket = StormContext.CustomerId.HasValue
                    ? new Basket {CustomerId = StormContext.CustomerId, CompanyId = StormContext.CompanyId}
                    : null;

                basket = client.ShoppingProxy.CreateBasket(basket, callerIpAddress, AccountId(null), PricelistSeed(null), CultureCode(null), Currency(null));

                StormContext.BasketId = basket.Id;
                StormContext.ConfirmedBasketId = null;

                return basket;
            }
        }

        public Basket AddItemToBasket(int basketId, BasketItem basketItem, int? createdBy = null, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                var basket = client.ShoppingProxy.GetBasket(basketId, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));

                //check if the basket already contains an item with with the given part no
                var existingItem = basket.Items.SingleOrDefault(itemInBasket => itemInBasket.PartNo == basketItem.PartNo);

                if (existingItem != null)
                {
                    existingItem.Quantity += basketItem.Quantity;
                    return client.ShoppingProxy.UpdateBasketItem(basketId, existingItem, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
                }

                //the item did not exist in the basket, add it.
                return client.ShoppingProxy.InsertBasketItem(basketId, basketItem, AccountId(createdBy), PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Basket UpdateBasket(Basket basket, int updatedBy, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateBasket(basket, updatedBy, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }            
        }

        public Basket UpdateBasketItem(int basketId, BasketItem basketItem, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateBasketItem(basketId, basketItem, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Basket DeleteBasketItem(int basketId, int lineNo, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.DeleteBasketItem(basketId, lineNo, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Basket ApplyDiscount(int basketId, string promotionCode, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            var basket = GetBasket(basketId);

            if (basket == null) throw new ArgumentException("Could not find basket with the given name");

            basket.DiscountCode = promotionCode;

            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateBasket(basket, AccountId(null), PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Basket RemovePromotion(int basketId, int promotionId, bool resetDiscountCode = false, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            var basket = GetBasket(basketId);

            if (basket == null) throw new ArgumentException("Could not find basket with the given name");

            //remove the promotion that was created
            var promotionToRemove = basket.AppliedPromotions.Single(promotion => promotion.Id == promotionId);

            basket.AppliedPromotions.Remove(promotionToRemove);

            if (resetDiscountCode) basket.DiscountCode = null;

            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateBasket(basket, AccountId(null), PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Checkout GetCheckout(int basketId, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId, bool useCache = true)
        {
            using (var client = GetBatch())
            {
                client.UseCache = useCache;
                return client.ShoppingProxy.GetCheckout(basketId, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Checkout SetCheckoutPaymentMethod(int basketId, int paymentMethodId, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdatePaymentMethod(basketId, paymentMethodId, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Checkout SetCheckoutDeliveryMethod(int basketId, int deliveryMethodId, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateDeliveryMethod(basketId, deliveryMethodId, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }            
        }

        public Checkout UpdateBuyer(int basketId, Customer buyer, int updatedBy = 1, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateBuyer(basketId, buyer, updatedBy, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            } 
        }

        public Checkout UpdatePayer(int basketId, Customer payer, int updatedBy = 1, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdatePayer(basketId, payer, updatedBy, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public Checkout UpdateShipto(int basketId, Customer shipTo, int updatedBy = 1, string priceListSeed = Ignored, string cultureCode = Ignored, int? currencyId = InvalidId)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.UpdateShipTo(basketId, shipTo, updatedBy, PricelistSeed(priceListSeed), CultureCode(cultureCode), Currency(currencyId));
            }
        }

        public PaymentResponse Purchase(int basketId, string callerIpAddress, string userAgent, NameValues paymentParameters = null, string priceListSeed = Ignored, string cultureCode = Ignored)
        {
            using (var client = GetBatch())
            {
                var paymentResponse = client.ShoppingProxy.PurchaseEx(basketId, callerIpAddress, userAgent, PricelistSeed(priceListSeed), CultureCode(cultureCode), paymentParameters);
                return paymentResponse;
            }
        }

        public XElement GetOrderRequest(int basketId, string cultureCode = Ignored)
        {
            using (var client = GetBatch())
            {
                return client.ShoppingProxy.GetOrderRequest(basketId, CultureCode(cultureCode));
            }
        }
    }
}
