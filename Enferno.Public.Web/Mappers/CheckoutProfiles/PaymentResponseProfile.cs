using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class PaymentResponseProfile: Profile
    {
        protected override void Configure()
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            Mapper.CreateMap<PaymentResponse, PaymentResponseModel>()
                .ForMember(to => to.Status, opts => opts.ResolveUsing<PaymentStatusResolver>())
                .ForMember(to => to.SuccessUrl, opts => opts.UseValue(siteRules.GetPaymentSuccessUrl()))
                .ForMember(to => to.HttpMethod, opts => opts.Ignore());
        }
    }

    public class PaymentStatusResolver : ValueResolver<PaymentResponse, PaymentStatus>
    {
        protected override PaymentStatus ResolveCore(PaymentResponse source)
        {
            switch (source.Status)
            {
                case "OK":
                    return PaymentStatus.Ok;
                case "ERROR":
                    return PaymentStatus.Error;
                default:
                    throw new ArgumentOutOfRangeException("source.Status", String.Format("Cannot map status string {0} to PaymentStatus enum", source.Status));
            }
        }
    }
}
