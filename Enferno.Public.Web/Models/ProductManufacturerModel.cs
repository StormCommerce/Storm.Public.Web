

using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ProductManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
    }
}
