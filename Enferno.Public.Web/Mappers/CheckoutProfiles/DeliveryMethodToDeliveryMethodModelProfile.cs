using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class DeliveryMethodToDeliveryMethodModelProfile : Profile
    {
        public DeliveryMethodToDeliveryMethodModelProfile()
        {
            
            CreateMap<DeliveryMethod, DeliveryMethodModel>()
                .ForMember(to => to.ImageUrl, opts => opts.MapFrom(from => Link.ImageUrl(from.ImageKey, null)));
        }
    }
}
