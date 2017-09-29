using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class DeliveryMethodModel
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsForCompanyOnly { get; set; }
        public bool IsForPersonOnly { get; set; }
        public bool IsNotifiable { get; set; }
        public bool IsSelected { get; set; }
        public int? LocationId { get; set; }
        public string Name { get; set; }
        public string PartNo { get; set; }
        public decimal Price { get; set; }
        public int? StoreId { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal VatRate { get; set; }
        public int? WarehouseId { get; set; }
    }
}
