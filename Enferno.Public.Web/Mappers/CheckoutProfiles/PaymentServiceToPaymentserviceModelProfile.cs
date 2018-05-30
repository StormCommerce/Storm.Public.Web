using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class PaymentServiceToPaymentserviceModelProfile: Profile
    {
        public PaymentServiceToPaymentserviceModelProfile()
        {
            CreateMap<PaymentService, PaymentServiceModel>()
                .ForMember(to => to.ImageUrl, opts => opts.MapFrom(from => Link.ImageUrl(from.ImageKey, null)));
        }
    }
}
