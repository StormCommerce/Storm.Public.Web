using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class PaymentMethodToPaymentMethodModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PaymentMethod, PaymentMethodModel>()
                .ForMember(to => to.ImageUrl, opts => opts.MapFrom(from => Link.ImageUrl(from.ImageKey, null)));
        }
    }
}
