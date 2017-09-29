
using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ProductModel
    {
        public ProductModel()
        {
            Variants = new List<VariantModel>();
            Flags = new List<int>();
            Files = new List<ProductFileModel>();
            Families = new List<ProductFamilyModel>();
            VariantParametrics = new List<ParametricModel>();
            Parametrics = new List<ParametricModel>();
            Products = new Dictionary<string, ProductListModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string UniqueName { get; set; }
        public string SubHeader { get; set; }
        public string DescriptionHeader { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }

        public int StatusId { get; set; }
        public string MetaTags { get; set; }
        public string MetaDescription { get; set; }
        public int? StockDisplayBreakPoint { get; set; }
        public bool IsBuyable { get; set; }

        public List<VariantModel> Variants { get; set; }
        public VariantModel SelectedVariant { get; set; }   
        
        public ProductManufacturerModel Manufacturer { get; set; }
        public ProductCategoryModel Category { get; set; }
        
        public List<int> Flags { get; set; }

        public decimal RecommendedQuantity { get; set; }
        public bool IsRecommendedQuantityFixed { get; set; }

        public List<ProductFileModel> Files { get; set; }
        public List<ProductFamilyModel> Families { get; set; }

        private PriceModel price;
        public PriceModel Price
        {
            get { return SelectedVariant != null ? SelectedVariant.Price : price; }
            set { price = value; }
        }

        private OnHandStatusModel onHandStatus;
        public OnHandStatusModel OnHandStatus
        {
            get { return SelectedVariant != null ? SelectedVariant.OnHandStatus : onHandStatus; }
            set { onHandStatus = value; }
        }

        public List<ParametricModel> VariantParametrics { get; set; }
        public List<ParametricModel> Parametrics { get; set; }

        /// <summary>
        /// Can be used to add ProductListModels such as Accessories or other lists. Just tag the list with a suitable name.
        /// </summary>
        public Dictionary<string, ProductListModel> Products { get; set; } 
    }
}
