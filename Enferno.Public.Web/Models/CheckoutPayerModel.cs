
using System;

namespace Enferno.Public.Web.Models
{
    public interface ICheckoutPayerModel
    {
        AddressModel InvoiceAddress { get; set; }
    }

    [Serializable]
    public abstract class CheckoutPayerModel : ICheckoutPayerModel
    {
        public int? CustomerId { get; set; }
        public AddressModel InvoiceAddress { get; set; }
    }

    [Serializable]
    public class CheckoutCompanyPayerModel : CheckoutPayerModel
    {
        public int? CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string OrganisationNumber { get; set; }
        public CompanyInformationModel CompanyInformation { get; set; }
    }

    [Serializable]
    public class CheckoutPrivatePayerModel : CheckoutPayerModel
    {
        public string SocialSecurityNumber { get; set; }
        public PersonInformationModel PersonInformation { get; set; }
    }
}
