using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class AddressModel
    {
        public int? Id { get; set; }
        public string CareOf { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Zip { get; set; }
    }
}
