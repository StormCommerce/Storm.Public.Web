
using System;
using System.Collections.Generic;
using Enferno.StormApiClient.ExposeProxy;

namespace Enferno.Public.Web.Models
{
    /// <summary>
    /// The filter model is used to create dynamic filtering of product lists. Each filter has a list of values that can be used to match against properties or parameters.
    /// </summary>
    [Serializable]
    public class FilterModel
    {
        public FilterModel()
        {
            Values = new List<FilterValueModel>();
        }

        public FilterModel(int id, FilterType type, ParametricValueType valueType, string name = null, string description = null, string uom = null) : this()
        {
            Id = id;
            Type = type;
            Uom = uom;
            Name = name;
            Description = description;
            ValueType = valueType;
        }

        /// <summary>
        /// Id maps to value in FilterCategory enum or parametric id. 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Type can be used to render different filter type differently.
        /// </summary>
        public FilterType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Unit of measurement. Should be appended to the value when displaying values.
        /// </summary>
        public string Uom { get; set; }
        public ParametricValueType ValueType { get; set; }

        public List<FilterValueModel> Values { get; set; }

        [Obsolete("Use hasInvalidCount instead. This one does not take total items inot account.")]
        public bool IsValid
        {
            get
            {
                return Type == FilterType.List ? 
                    Values.Count > 1 && Values.TrueForAll(v => v.IsValid):
                    Values.Count == 1 && Values[0].IsValid;
            }            
        }

        public bool HasValidCount(int? itemCount)
        {
            return Type == FilterType.List ?
                Values.Count > (itemCount.HasValue ? 0 : 1) && Values.TrueForAll(v => v.HasValidCount(itemCount)) :
                Values.Count == 1 && Values[0].IsValid;
        }

        public string GetFilterName()
        {
            return $"Filter{(FilterCategory) Id}";
        }
    }
}
