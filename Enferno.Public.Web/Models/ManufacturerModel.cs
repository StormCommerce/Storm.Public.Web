
using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ManufacturerModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string UniqueName { get; set; }

        public string OrgNo { get; set; }
        public string ImageUrl { get; set; }
        public string SupportDescription { get; set; }
        public string SupportEmail { get; set; }
        public string SupportOpenHours { get; set; }
        public string SupportPhone { get; set; }
        public string SupportPolicy { get; set; }
        public string SupportUrl { get; set; }
        public string WebSite { get; set; }
    }
}
