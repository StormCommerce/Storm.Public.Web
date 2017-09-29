
using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    /// <summary>
    /// The ProductListModel can be used on product list pages.
    /// </summary>
    [Serializable]
    public class ProductListModel
    {
        public ProductListModel()
        {
            Items = new List<ProductItemModel>();
            Filters = new List<FilterModel>();
        }

        /// <summary>
        /// ListParametric can be used as parameter when listing products by parametrics. 
        /// Specify the parametric Id and set either ValueId or ValueFrom/ValueTo.
        /// ValueTo and ValueFrom can be used to specify value ranges. If no range is wanted, just specify ValueFrom.
        /// </summary>
        public class ListParametric
        {
            public int Id { get; set; }
            public int? ValueId { get; set; }
            public string ValueFrom { get; set; }
            public string ValueTo { get; set; }
        }

        /// <summary>
        /// This is the total item count regardless of filtering and paging, while Items.Count is the actual number of items returened.
        /// </summary>
        public int ItemCount { get; set; }

        public List<ProductItemModel> Items { get; set; }

        public List<FilterModel> Filters { get; set; }
    }
}
