using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public PersonInformationModel Person { get; set; }

        public AddressModel InvoiceAddress { get; set; }
        public IEnumerable<AddressModel> DeliveryAddresses { get; set; }
        public bool UseInvoiceAddressAsDeliveryAddress { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; } 
    }
}
