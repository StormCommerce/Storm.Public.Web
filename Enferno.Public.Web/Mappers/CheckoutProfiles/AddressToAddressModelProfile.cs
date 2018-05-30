
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Customers;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class AddressToAddressModelProfile : Profile
    {
        public AddressToAddressModelProfile()
        {
            CreateMap<Address, AddressModel>();

            CreateMap<AddressModel, Address>()
                .ForMember(to => to.Region, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore())
                .ForMember(to => to.IsValidated, opts => opts.Ignore())
                .ForMember(to => to.GlobalLocationNo, opts => opts.Ignore());
        }
    }
}
