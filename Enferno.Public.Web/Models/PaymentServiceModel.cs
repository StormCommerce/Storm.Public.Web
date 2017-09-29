using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class PaymentServiceModel
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
