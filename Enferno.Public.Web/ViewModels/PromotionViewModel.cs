using System;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class PromotionViewModel
    {
        public int? Id { get; set; }
        public string Header { get; set; }
        public string ShortDescription { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string DiscountCode { get; set; }
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string ImageUrl { get; set; }

        public string RequirementSeed { get; set; }
    }
}
