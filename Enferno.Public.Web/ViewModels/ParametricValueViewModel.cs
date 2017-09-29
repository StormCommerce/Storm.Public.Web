using System;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class ParametricValueViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public int SortOrder { get; set; }
    }
}
