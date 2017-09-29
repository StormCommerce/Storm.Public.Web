
using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    /// <summary>
    /// The Variant model keeps information about what is specific to the variant. Common data is at ProductModel level.
    /// </summary>
    [Serializable]
    public class VariantModel
    {
        public VariantModel()
        {
            Files = new List<ProductFileModel>();
            Flags = new List<int>();
            VariantParametrics = new List<ParametricModel>();
            Parametrics = new List<ParametricModel>();
        }

        public int Id { get; set; }
        /// <summary>
        /// The name of the variant
        /// </summary>
        public string Name { get; set; }
        public string PartNo { get; set; }
        public string ManufacturePartNo { get; set; }

        public PriceModel Price { get; set; }

        public OnHandStatusModel OnHandStatus { get; set; }
        public string ImageKey { get; set; }

        /// <summary>
        /// The ImageUrl is the Url to the image. It is resolved to an actual address but has no informationa about size. This has to be provided by the application.
        /// If Enferno's image server is used a preset neddds to be apended to the url by the application.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Flags are variant specific flags. Common flags are kept at Product level. 
        /// </summary>
        public List<int> Flags { get; set; }

        /// <summary>
        /// Files are variant specific files. Common files are kept at Product level. 
        /// </summary>
        public List<ProductFileModel> Files { get; set; }

        /// <summary>
        /// VariantParametrics are the parameters that distingushes variante from each other.
        /// </summary>
        public List<ParametricModel> VariantParametrics { get; set; }

        /// <summary>
        /// Parameterics are the parameters that differ amongst variants, but still can be different amongst variants
        /// </summary>
        public List<ParametricModel> Parametrics { get; set; }
    }
}
