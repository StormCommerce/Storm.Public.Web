using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class CompanyModel
    {
        public IEnumerable<AddressModel> DeliveryAddresses { get; set; }
        public AddressModel InvoiceAddress { get; set; }
        public CompanyInformationModel CompanyInformation { get; set; }
        public bool UseInvoiceAddressAsDeliveryAddress { get; set; }
    }
}
