
using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ProductItemModel
    {
        public ProductItemModel()
        {
            Files = new List<ProductFileModel>();
            Variants = new List<VariantModel>();
            Parametrics = new List<ParametricModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string UniqueName { get; set; }
        public string SubHeader { get; set; }

        public ProductManufacturerModel Manufacturer { get; set; }
        public List<int> Flags { get; set; }

        public PriceModel Price { get; set; }
        public decimal RecommendedQuantity { get; set; }
        public bool IsRecommendedQuantityFixed { get; set; }

        public List<ProductFileModel> Files { get; set; }
        public List<VariantModel> Variants { get; set; }

        public OnHandStatusModel OnHandStatus { get; set; }

        public int CategoryId { get; set; }
        public int? PopularityRank { get; set; }

        public int StatusId { get; set; }

        public List<ParametricModel> Parametrics { get; set; }

        public string Url { get; set; }    
    }
}
