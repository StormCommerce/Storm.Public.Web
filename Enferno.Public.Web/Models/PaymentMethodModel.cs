using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsForCompanyOnly { get; set; }
        public bool IsForPersonOnly { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string PartNo { get; set; }
        public decimal Price { get; set; }
        public PaymentServiceModel Service { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal VatRate { get; set; }
    }
}
