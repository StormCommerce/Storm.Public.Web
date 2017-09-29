using AutoMapper;
using Enferno.Public.Web.Mappers.BasketProfiles;
using Enferno.Public.Web.Mappers.CheckoutProfiles;
using Enferno.Public.Web.Mappers.ProductProfiles;
using Enferno.Public.Web.Mappers.PromotionProfiles;

namespace Enferno.Public.Web.Mappers
{
    public class WebProfile : Profile
    {
        protected override void Configure()
        {
            //Basket
            Mapper.Configuration.AddProfile<BasketToBasketModelProfile>();
            Mapper.Configuration.AddProfile<BasketItemModelToBasketItemProfile>();
            Mapper.Configuration.AddProfile<BasketModelToBasketProfile>();
            Mapper.Configuration.AddProfile<BasketItemToBasketItemModelProfile>();

            //Checkout
            Mapper.Configuration.AddProfile<AddressToAddressModelProfile>();
            Mapper.Configuration.AddProfile<CheckoutToCheckoutModelProfile>();
            Mapper.Configuration.AddProfile<CompanyToCompanyModelProfile>();
            Mapper.Configuration.AddProfile<CustomerToCustomerModelProfile>();
            Mapper.Configuration.AddProfile<DeliveryMethodToDeliveryMethodModelProfile>();
            Mapper.Configuration.AddProfile<PaymentMethodToPaymentMethodModelProfile>();
            Mapper.Configuration.AddProfile<PaymentServiceToPaymentserviceModelProfile>();
            Mapper.Configuration.AddProfile<PaymentResponseProfile>();

            //Product
            Mapper.Configuration.AddProfile<ProductProfile>();

            //Promotion
            Mapper.Configuration.AddProfile<PromotionProfile>();

            //Manufacturer
            Mapper.Configuration.AddProfile<ManufacturerToManufacturerModelProfile>();
        }
    }
}
