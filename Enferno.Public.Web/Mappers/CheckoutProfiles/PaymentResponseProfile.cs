using System;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class PaymentResponseProfile: Profile
    {
        public PaymentResponseProfile()
        {
            var siteRules = IoC.Resolve<ISiteRules>();

            CreateMap<PaymentResponse, PaymentResponseModel>()
                .ForMember(to => to.Status, opts => opts.ResolveUsing<PaymentStatusResolver>())
                .ForMember(to => to.SuccessUrl, opts => opts.UseValue(siteRules.GetPaymentSuccessUrl()))
                .ForMember(to => to.HttpMethod, opts => opts.Ignore());
        }
    }

    public class PaymentStatusResolver : IValueResolver<PaymentResponse, PaymentResponseModel, PaymentStatus>
    {
        public PaymentStatus Resolve(PaymentResponse source, PaymentResponseModel destination, PaymentStatus destMember, ResolutionContext context)
        {
            switch (source.Status)
            {
                case "OK":
                    return PaymentStatus.Ok;
                case "ERROR":
                    return PaymentStatus.Error;
                default:
                    throw new ArgumentOutOfRangeException("source.Status", $"Cannot map status string {source.Status} to PaymentStatus enum");
            }
        }
    }
}
