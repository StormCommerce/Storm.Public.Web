using System.Xml.Linq;
using Enferno.StormApiClient;
using Enferno.StormApiClient.Customers;
using Enferno.StormApiClient.Expose;
using Enferno.StormApiClient.Products;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Repositories
{
    public interface IShoppingRepository
    {
        Basket GetBasket(int id, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);

        ProductOnHandList GetOnHandForBasketProducts(int basketId, Warehouse warehouse, string pricelistSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);

        ProductOnHandList GetExternalOnHandForBasketProducts(int basketId, Warehouse warehouse, string pricelistSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Basket CreateBasket(string callerIpAddress);

        Basket AddItemToBasket(int basketId, BasketItem basketItem, int? createdBy = null, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Basket UpdateBasket(Basket basket, int updatedBy, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Basket UpdateBasketItem(int basketId, BasketItem basketItem, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Basket DeleteBasketItem(int basketId, int lineNo, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Basket ApplyDiscount(int basketId, string promotionCode, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Basket RemovePromotion(int basketId, int promotionId, bool resetDiscountCode = false, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Checkout GetCheckout(int basketId, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId, bool useCache = true);

        Checkout SetCheckoutPaymentMethod(int basketId, int paymentMethodId, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Checkout SetCheckoutDeliveryMethod(int basketId, int deliveryMethodId, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Checkout UpdateBuyer(int basketId, Customer buyer, int updatedBy = 1, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Checkout UpdatePayer(int basketId, Customer payer, int updatedBy = 1, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        Checkout UpdateShipto(int basketId, Customer shipTo, int updatedBy = 1, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored, int? currencyId = RepositoryBase.InvalidId);

        PaymentResponse Purchase(int basketId, string callerIpAddress, string userAgent, NameValues paymentParameters = null, string priceListSeed = RepositoryBase.Ignored, string cultureCode = RepositoryBase.Ignored);

        XElement GetOrderRequest(int basketId, string cultureCode = RepositoryBase.Ignored);
    }
}
