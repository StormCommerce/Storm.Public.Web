
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Builders
{
    public class CheckoutBuilder : BuilderBase
    {
        public CheckoutModelBase MapCheckoutModel(Checkout checkout)
        {
            var isPrivate = checkout.Payer.Companies == null || checkout.Payer.Companies.Count == 0;

            if (isPrivate)
            {
                return Mapper.Map<Checkout, PrivateCheckoutModel>(checkout);
            }

            return Mapper.Map<Checkout, CompanyCheckoutModel>(checkout);
        }


    }
}
