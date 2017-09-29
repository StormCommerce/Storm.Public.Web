using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class CheckoutBuyerModel : PersonInformationModel
    {
        public int? CustomerId { get; set; }
    }
}
