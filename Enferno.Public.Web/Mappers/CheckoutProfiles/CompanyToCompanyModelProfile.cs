using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Customers;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class CompanyToCompanyModelProfile : Profile
    {
        public CompanyToCompanyModelProfile()
        {

            CreateMap<Company, CompanyInformationModel>();

            CreateMap<CompanyInformationModel, Company>()
                .ForMember(to => to.Id, opts => opts.Ignore())
                .ForMember(to => to.Key, opts => opts.Ignore())
                .ForMember(to => to.Code, opts => opts.Ignore())
                .ForMember(to => to.OrgNo, opts => opts.Ignore())
                .ForMember(to => to.ReferId, opts => opts.Ignore())
                .ForMember(to => to.ReferUrl, opts => opts.Ignore())
                .ForMember(to => to.DeliveryAddresses, opts => opts.Ignore())
                .ForMember(to => to.InvoiceAddress, opts => opts.Ignore())
                .ForMember(to => to.ParentId, opts => opts.Ignore())
                .ForMember(to => to.DeliveryMethodIds, opts => opts.Ignore())
                .ForMember(to => to.PaymentMethodIds, opts => opts.Ignore())
                .ForMember(to => to.UseInvoiceAddressAsDeliveryAddress, opts => opts.Ignore())
                .ForMember(to => to.Info, opts => opts.Ignore())
                .ForMember(to => to.PricelistIds, opts => opts.Ignore())
                .ForMember(to => to.Email, opts => opts.Ignore())
                .ForMember(to => to.Flags, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore())
                .ForMember(to => to.VatNo, opts => opts.Ignore());              

            CreateMap<Company, CompanyModel>()
                .ForMember(to => to.CompanyInformation,
                    opts => opts.MapFrom(from => Mapper.Map<Company, CompanyInformationModel>(from)));
        }
    }
}
