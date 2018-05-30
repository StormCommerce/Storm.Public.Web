using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Customers;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class CustomerToCustomerModelProfile : Profile
    {
        public CustomerToCustomerModelProfile()
        {
            CreateMap<Customer, PersonInformationModel>()
                .Include<Customer, CheckoutBuyerModel>();

            CreateMap<Customer, CheckoutBuyerModel>()
                .ForMember(to => to.CustomerId, opts => opts.MapFrom(from => from.Id));

            CreateMap<CheckoutBuyerModel, Customer>()
                .ForMember(to => to.Id, opts => opts.MapFrom(from => from.CustomerId))
                .ForMember(to => to.FirstName, opts => opts.Condition(from => from.FirstName != null))
                .ForMember(to => to.LastName, opts => opts.Condition(from => from.LastName != null))
                .ForMember(to => to.Email, opts => opts.Condition(from => from.Email != null))
                .ForMember(to => to.Phone, opts => opts.Condition(from => from.Phone != null))
                .ForMember(to => to.CellPhone, opts => opts.Condition(from => from.CellPhone != null))
                .ForMember(to => to.Key, opts => opts.Ignore())
                .ForMember(to => to.Code, opts => opts.Ignore())
                .ForMember(to => to.SSN, opts => opts.Ignore())
                .ForMember(to => to.ReferId, opts => opts.Ignore())
                .ForMember(to => to.ReferUrl, opts => opts.Ignore())
                .ForMember(to => to.Account, opts => opts.Ignore())
                .ForMember(to => to.Companies, opts => opts.Ignore())
                .ForMember(to => to.DeliveryAddresses, opts => opts.Ignore())
                .ForMember(to => to.InvoiceAddress, opts => opts.Ignore())
                .ForMember(to => to.Flags, opts => opts.Ignore())
                .ForMember(to => to.UseInvoiceAddressAsDeliveryAddress, opts => opts.Ignore())
                .ForMember(to => to.Info, opts => opts.Ignore())
                .ForMember(to => to.PricelistIds, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore());

            CreateMap<Customer, CustomerModel>()
                .ForMember(to => to.Person,
                    opts =>
                        opts.MapFrom(from => Mapper.Map<Customer, PersonInformationModel>(from)));
        }

        public override string ProfileName => GetType().Name;
    }  
}
