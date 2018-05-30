using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Customers;
using Enferno.StormApiClient.Shopping;

namespace Enferno.Public.Web.Mappers.CheckoutProfiles
{
    public class CheckoutToCheckoutModelProfile : Profile
    {
        public CheckoutToCheckoutModelProfile()
        {
            //Customer -> CheckoutPayerModel
            CreateMap<Customer, CheckoutPayerModel>()
                .ForMember(to => to.CustomerId, opts => opts.MapFrom(from => from.Id))
                .Include<Customer, CheckoutPrivatePayerModel>()
                .Include<Customer, CheckoutCompanyPayerModel>();

            CreateMap<Customer, CheckoutPrivatePayerModel>()
                .ForMember(to => to.PersonInformation,
                    opts => opts.MapFrom(from => Mapper.Map<Customer, PersonInformationModel>(from)))
                .ForMember(to => to.SocialSecurityNumber, opts => opts.MapFrom(from => from.SSN));

            CreateMap<Customer, CheckoutCompanyPayerModel>()
                .ForMember(to => to.InvoiceAddress,
                    opts => opts.MapFrom(from => from.Companies != null && from.Companies.Count > 0
                        ? from.Companies[0].InvoiceAddress
                        : null))
                .ForMember(to => to.CompanyId,
                    opts =>
                        opts.MapFrom(
                            from => from.Companies != null && from.Companies.Count > 0
                                ? from.Companies[0].Id
                                : null))
                .ForMember(to => to.CompanyCode,
                    opts =>
                        opts.MapFrom(
                            from => from.Companies != null && from.Companies.Count > 0
                                ? from.Companies[0].Code
                                : null))
                .ForMember(to => to.CompanyInformation,
                    opts =>
                        opts.MapFrom(
                            from =>
                                from.Companies != null && from.Companies.Count > 0
                                    ? Mapper.Map<Company, CompanyInformationModel>(from.Companies[0])
                                    : null))
                .ForMember(to => to.OrganisationNumber,
                    opts =>
                        opts.MapFrom(
                            from =>
                                from.Companies != null && from.Companies.Count > 0
                                    ? from.Companies[0].OrgNo
                                    : null));

            //CheckoutPayerModel -> Customer
            CreateMap<CheckoutPayerModel, Customer>()
                .ForMember(to => to.Id, opts => opts.MapFrom(from => from.CustomerId))
                .ForMember(to => to.Key, opts => opts.Ignore())
                .ForMember(to => to.Code, opts => opts.Ignore())
                .ForMember(to => to.SSN, opts => opts.Ignore())
                .ForMember(to => to.FirstName, opts => opts.Ignore())
                .ForMember(to => to.LastName, opts => opts.Ignore())
                .ForMember(to => to.Email, opts => opts.Ignore())
                .ForMember(to => to.Phone, opts => opts.Ignore())
                .ForMember(to => to.CellPhone, opts => opts.Ignore())
                .ForMember(to => to.ReferId, opts => opts.Ignore())
                .ForMember(to => to.ReferUrl, opts => opts.Ignore())
                .ForMember(to => to.Account, opts => opts.Ignore())
                .ForMember(to => to.Companies, opts => opts.Ignore())
                .ForMember(to => to.DeliveryAddresses, opts => opts.Ignore())
                .ForMember(to => to.Flags, opts => opts.Ignore())
                .ForMember(to => to.UseInvoiceAddressAsDeliveryAddress, opts => opts.Ignore())
                .ForMember(to => to.Info, opts => opts.Ignore())
                .ForMember(to => to.PricelistIds, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore())
                .Include<CheckoutPrivatePayerModel, Customer>()
                .Include<CheckoutCompanyPayerModel, Customer>();

            CreateMap<CheckoutPrivatePayerModel, Customer>()
                .ForMember(to => to.Email,
                    opts => opts.MapFrom(from => from.PersonInformation.Email))
                .ForMember(to => to.FirstName,
                    opts => opts.MapFrom(from => from.PersonInformation.FirstName))
                .ForMember(to => to.LastName,
                    opts => opts.MapFrom(from => from.PersonInformation.LastName))
                .ForMember(to => to.CellPhone,
                    opts => opts.MapFrom(from => from.PersonInformation.CellPhone))
                .ForMember(to => to.Phone,
                    opts => opts.MapFrom(from => from.PersonInformation.Phone))
                .ForMember(to => to.SSN, opts => opts.MapFrom(from => from.SocialSecurityNumber))
                .ForMember(to => to.Companies, opts => opts.Ignore())
                .ForMember(to => to.PricelistIds, opts => opts.Ignore());

            CreateMap<CheckoutCompanyPayerModel, Customer>()
                .BeforeMap((from, to) =>
                {
                    var company = Mapper.Map<CompanyInformationModel, Company>(from.CompanyInformation);
                    if (to.Companies != null && to.Companies.Count > 0)
                    {
                        company = to.Companies[0];
                        Mapper.Map(from.CompanyInformation, company);
                    }
                    company.Id = from.CompanyId;
                    company.Code = from.CompanyCode;
                    company.OrgNo = from.OrganisationNumber;
                    company.InvoiceAddress = Mapper.Map<AddressModel, Address>(from.InvoiceAddress);
                    if (to.Companies == null || to.Companies.Count == 0)
                    {
                        to.Companies = new CompanyList {company};
                    }
                })
                .ForMember(to => to.PricelistIds, opts => opts.Ignore());

            //Customer -> CheckoutShipToModel
            CreateMap<Customer, CheckoutShipToModel>()
                .ForMember(to => to.CustomerId, opts => opts.MapFrom(from => from.Id))
                .ForMember(to => to.DeliveryAddress, opts => opts.Ignore())
                .Include<Customer, CheckoutPrivateShipToModel>()
                .Include<Customer, CheckoutCompanyShipToModel>();

            CreateMap<Customer, CheckoutPrivateShipToModel>()
                .ForMember(to => to.DeliveryAddress,
                    opts =>
                        opts.MapFrom(
                            from =>
                                from.DeliveryAddresses != null && from.DeliveryAddresses.Count > 0
                                    ? from.DeliveryAddresses[0]
                                    : null))
                .ForMember(to => to.PersonInformation,
                    opts => opts.MapFrom(from => Mapper.Map<Customer, PersonInformationModel>(from)))
                .ForMember(to => to.SocialSecurityNumber, opts => opts.MapFrom(from => from.SSN));

            CreateMap<Customer, CheckoutCompanyShipToModel>()
                .ForMember(to => to.DeliveryAddress,
                    opts =>
                        opts.MapFrom(
                            from =>
                                from.Companies != null && from.Companies.Count > 0 &&
                                from.Companies[0].DeliveryAddresses != null &&
                                from.Companies[0].DeliveryAddresses.Count > 0
                                    ? from.Companies[0].DeliveryAddresses[0]
                                    : null))
                .ForMember(to => to.CompanyId,
                    opts =>
                        opts.MapFrom(
                            from => from.Companies != null && from.Companies.Count > 0
                                ? from.Companies[0].Id
                                : null))
                .ForMember(to => to.CompanyCode,
                    opts =>
                        opts.MapFrom(
                            from => from.Companies != null && from.Companies.Count > 0
                                ? from.Companies[0].Code
                                : null))
                .ForMember(to => to.CompanyInformation,
                    opts =>
                        opts.MapFrom(
                            from =>
                                from.Companies != null && from.Companies.Count > 0
                                    ? Mapper.Map<Company, CompanyInformationModel>(from.Companies[0])
                                    : null))
                .ForMember(to => to.OrganisationNumber,
                    opts =>
                        opts.MapFrom(
                            from =>
                                from.Companies != null && from.Companies.Count > 0
                                    ? from.Companies[0].OrgNo
                                    : null));

            //CheckoutShipToModel -> Customer
            CreateMap<CheckoutShipToModel, Customer>()
                .ForMember(to => to.Id, opts => opts.MapFrom(from => from.CustomerId))
                .ForMember(to => to.Key, opts => opts.Ignore())
                .ForMember(to => to.Code, opts => opts.Ignore())
                .ForMember(to => to.SSN, opts => opts.Ignore())
                .ForMember(to => to.FirstName, opts => opts.Ignore())
                .ForMember(to => to.LastName, opts => opts.Ignore())
                .ForMember(to => to.Email, opts => opts.Ignore())
                .ForMember(to => to.Phone, opts => opts.Ignore())
                .ForMember(to => to.CellPhone, opts => opts.Ignore())
                .ForMember(to => to.ReferId, opts => opts.Ignore())
                .ForMember(to => to.ReferUrl, opts => opts.Ignore())
                .ForMember(to => to.Account, opts => opts.Ignore())
                .ForMember(to => to.Companies, opts => opts.Ignore())
                .ForMember(to => to.InvoiceAddress, opts => opts.Ignore())
                .ForMember(to => to.DeliveryAddresses, opts => opts.Ignore())
                .ForMember(to => to.Flags, opts => opts.Ignore())
                .ForMember(to => to.UseInvoiceAddressAsDeliveryAddress, opts => opts.Ignore())
                .ForMember(to => to.Info, opts => opts.Ignore())
                .ForMember(to => to.PricelistIds, opts => opts.Ignore())
                .ForMember(to => to.ExtensionData, opts => opts.Ignore())
                .Include<CheckoutPrivateShipToModel, Customer>()
                .Include<CheckoutCompanyShipToModel, Customer>();

            CreateMap<CheckoutPrivateShipToModel, Customer>()
                .ForMember(to => to.Email, opts => opts.MapFrom(from => from.PersonInformation.Email))
                .ForMember(to => to.FirstName, opts => opts.MapFrom(from => from.PersonInformation.FirstName))
                .ForMember(to => to.LastName, opts => opts.MapFrom(from => from.PersonInformation.LastName))
                .ForMember(to => to.CellPhone, opts => opts.MapFrom(from => from.PersonInformation.CellPhone))
                .ForMember(to => to.Phone, opts => opts.MapFrom(from => from.PersonInformation.Phone))
                .ForMember(to => to.DeliveryAddresses, opts => opts.MapFrom(from => new AddressList {Mapper.Map<AddressModel, Address>(from.DeliveryAddress)}))
                .ForMember(to => to.PricelistIds, opts => opts.Ignore());

            CreateMap<CheckoutCompanyShipToModel, Customer>()
                .BeforeMap((from, to) =>
                {
                    var company = Mapper.Map<CompanyInformationModel, Company>(from.CompanyInformation);
                    if (to.Companies != null && to.Companies.Count > 0)
                    {
                        company = to.Companies[0];
                        Mapper.Map(from.CompanyInformation, company);
                    }
                    company.Id = from.CompanyId;
                    company.Code = from.CompanyCode;
                    company.OrgNo = from.OrganisationNumber;
                    company.DeliveryAddresses = new AddressList
                    {
                        Mapper.Map<AddressModel, Address>(from.DeliveryAddress)
                    };
                    if (to.Companies == null || to.Companies.Count == 0)
                    {
                        to.Companies = new CompanyList {company};
                    }
                })
                .ForMember(to => to.PricelistIds, opts => opts.Ignore());


            //Checkout -> PrivateCheckoutModel
            CreateMap<Checkout, PrivateCheckoutModel>()
                .ForMember(to => to.Buyer,
                    opts => opts.MapFrom(
                        from =>
                            Mapper.Map<Customer, CheckoutBuyerModel>(from.Buyer)))
                .ForMember(to => to.Payer,
                    opts =>
                        opts.MapFrom(
                            from =>
                                Mapper.Map<Customer, CheckoutPrivatePayerModel>(from.Payer)))
                .ForMember(to => to.ShipTo,
                    opts =>
                        opts.MapFrom(
                            from =>
                                Mapper.Map<Customer, CheckoutPrivateShipToModel>(from.ShipTo)))
                .ForMember(to => to.OrderReference, opts => opts.MapFrom(from => from.Basket.OrderReference));


            //Checkout -> CompanyCheckoutModel
            CreateMap<Checkout, CompanyCheckoutModel>()
                .ForMember(to => to.Buyer,
                    opts => opts.MapFrom(
                        from =>
                            Mapper.Map<Customer, CheckoutBuyerModel>(from.Buyer)))
                .ForMember(to => to.Payer,
                    opts =>
                        opts.MapFrom(
                            from =>
                                Mapper.Map<Customer, CheckoutCompanyPayerModel>(from.Payer)))
                .ForMember(to => to.ShipTo,
                    opts =>
                        opts.MapFrom(
                            from =>
                                Mapper.Map<Customer, CheckoutCompanyShipToModel>(from.ShipTo)))
                .ForMember(to => to.OrderReference, opts => opts.MapFrom(from => from.Basket.OrderReference));


        }

        public override string ProfileName => GetType().Name;
    }
}
