using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class VariantViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartNo { get; set; }
        public string ManufacturePartNo { get; set; }

        public decimal DisplayPrice { get; set; }
        public decimal CatalogPrice { get; set; }
        public decimal VatRate { get; set; }
        public int PriceListId { get; set; }

        public OnHandStatus OnHandStatus { get; set; }
        public string OnHandStatusText1 { get; set; }
        public string OnHandStatusText2 { get; set; }
        public decimal OnHandStatusCount { get; set; }

        public string ImageUrl { get; set; }

        /// <summary>
        /// Flags are variant specific flags. Common flags are kept at Product level. 
        /// </summary>
        public List<int> Flags { get; set; }

        /// <summary>
        /// Files are variant specific files. Common files are kept at Product level. 
        /// </summary>
        public List<FileViewModel> Files { get; set; }

        /// <summary>
        /// VariantParametrics are the parameters that distingushes variante from each other.
        /// </summary>
        public List<ParametricViewModel> VariantParametrics { get; set; }

        /// <summary>
        /// Parameterics are the parameters that differ amongst variants, but still can be different amongst variants
        /// </summary>
        public List<ParametricViewModel> Parametrics { get; set; }
    }
}
