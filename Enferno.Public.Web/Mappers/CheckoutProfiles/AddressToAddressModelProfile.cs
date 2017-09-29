using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Customers;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class AddressToAddressModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Address, AddressModel>();

            Mapper.CreateMap<AddressModel, Address>()
                .ForMember(to => to.Region, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore());
        }
    }
}
