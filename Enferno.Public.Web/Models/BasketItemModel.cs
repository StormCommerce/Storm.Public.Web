using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class BasketItemModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string SubHeader { get; set; }
        public decimal Quantity { get; set; }
        public decimal? RecommendedQuantity { get; set; }
        public bool IsRecommendedQuantityFixed { get; set; }
        public int LineNo { get; set; }
        public PriceModel Price { get; set; }
        public string ProductUrl { get; set; }

        public string PartNo { get; set; }
        public int PriceListId { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDiscountable { get; set; }
        public ProductType Type { get; set; }
    }
}
