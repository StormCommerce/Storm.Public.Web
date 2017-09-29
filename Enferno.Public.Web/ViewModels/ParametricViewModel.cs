using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class ParametricViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool IsPrimary { get; set; }
        public int GroupId { get; set; }
        public string Group { get; set; }
        public List<ParametricValueViewModel> Values { get; set; }
    }
}
