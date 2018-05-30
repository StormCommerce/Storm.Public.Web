using AutoMapper;
using Enferno.Public.Web.Mappers.BasketProfiles;
using Enferno.Public.Web.Mappers.CheckoutProfiles;
using Enferno.Public.Web.Mappers.ProductProfiles;
using Enferno.Public.Web.Mappers.PromotionProfiles;

namespace Enferno.Public.Web.Mappers
{
    public class WebProfile : Profile
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //Basket
                cfg.AddProfile<BasketToBasketModelProfile>();
                cfg.AddProfile<BasketItemModelToBasketItemProfile>();
                cfg.AddProfile<BasketModelToBasketProfile>();
                cfg.AddProfile<BasketItemToBasketItemModelProfile>();

                //Checkout
                cfg.AddProfile<AddressToAddressModelProfile>();
                cfg.AddProfile<CheckoutToCheckoutModelProfile>();
                cfg.AddProfile<CompanyToCompanyModelProfile>();
                cfg.AddProfile<CustomerToCustomerModelProfile>();
                cfg.AddProfile<DeliveryMethodToDeliveryMethodModelProfile>();
                cfg.AddProfile<PaymentMethodToPaymentMethodModelProfile>();
                cfg.AddProfile<PaymentServiceToPaymentserviceModelProfile>();
                cfg.AddProfile<PaymentResponseProfile>();

                //Product
                cfg.AddProfile<ProductManufacturerToProductManufacturerModelProfile>();
                cfg.AddProfile<ProductFileToProductFileModelProfile>();
                cfg.AddProfile<ProductItemToProductItemModelProfile>();
                cfg.AddProfile<ProductItemToVariantModelProfile>();
                cfg.AddProfile<VariantItemToVariantModelProfile>();
                cfg.AddProfile<ProductToVariantModelProfile>();
                cfg.AddProfile<ProductToProductModelProfile>();

                cfg.AddProfile<ManufacturerToManufacturerModelProfile>();

                cfg.AddProfile<ProductModelToProductViewModelProfile>();
                cfg.AddProfile<ProductFileModelToFileViewModelProfile>();
                cfg.AddProfile<ParametricModelToParametricViewModelProfile>();
                cfg.AddProfile<ParametricValueModelToParametricValueViewModelProfile>();
                cfg.AddProfile<VariantModelToVariantViewModelProfile>();

                //Promotion
                cfg.AddProfile<PromotionToPromotionModelProfile>();
                cfg.AddProfile<PromotionModelToPromotionProfile>();
                cfg.AddProfile<PromotionModelToPromotionViewModelProfile>();
                cfg.AddProfile<PromotionViewModelToPromotionModelProfile>();

                //Manufacturer

                cfg.AddProfile<WarehouseModelToWarehouseProfile>();
                cfg.AddProfile<WarehouseToWarehouseModelProfile>();
            });
        }
    }
}
