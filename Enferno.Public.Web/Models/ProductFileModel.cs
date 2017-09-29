
using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ProductFileModel
    {
        public ProductFileModel()
        {
        }

        public ProductFileModel(ProductFileType productFileType, string url, string altText)
            : this((int)productFileType, url, altText)
        {
        }

        public ProductFileModel(int type, string url, string altText)
        {
            Type = type;
            Url = url;
            AltText = altText;
        }

        public int Type { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
    }
}
