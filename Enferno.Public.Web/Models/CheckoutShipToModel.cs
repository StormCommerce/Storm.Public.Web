
using System;

namespace Enferno.Public.Web.Models
{
    public interface ICheckoutShipToModel
    {
        AddressModel DeliveryAddress { get; set; }
    }

    [Serializable]
    public abstract class CheckoutShipToModel : ICheckoutShipToModel
    {
        public int? CustomerId { get; set; }
        public AddressModel DeliveryAddress { get; set; }
    }

    [Serializable]
    public class CheckoutCompanyShipToModel : CheckoutShipToModel
    {
        public int? CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string OrganisationNumber { get; set; }
        public CompanyInformationModel CompanyInformation { get; set; }
    }

    [Serializable]
    public class CheckoutPrivateShipToModel : CheckoutShipToModel
    {
        public string SocialSecurityNumber { get; set; }
        public PersonInformationModel PersonInformation { get; set; }
    }
}
