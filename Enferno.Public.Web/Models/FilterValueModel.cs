

using System;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Models
{
    /// <summary>
    /// Base class for different filter values types
    /// </summary>
    [Serializable]
    public abstract class FilterValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }        
        public int SortOrder { get; set; }

        public abstract bool IsValid { get; }
    }

    [Serializable]
    public class FilterValueModel<T> : FilterValueModel
    {
        public FilterValueModel()
        {
        }

        public FilterValueModel(T value, ParametricValueModel parametricValue)
        {
            Value = value;
            Name = parametricValue.Name;
            Description = parametricValue.Description;
            Code = parametricValue.Code;
            SortOrder = parametricValue.SortOrder;
            ImageUrl = parametricValue.ImageUrl;

        }

        /// <summary>
        /// Counts the total items in the productlist matched by this value. 
        /// </summary>
        public int Count { get; set; }
        public T Value { get; set; }

        public override bool IsValid => Count > 0;
    }

    [Serializable]
    public class RangeFilterValueModel<T> : FilterValueModel where T: IComparable
    {
        public RangeFilterValueModel()
        {
        }

        public RangeFilterValueModel(T from, T to, FilterRangeItem rangeItem)
        {
            From = from;
            To = to;
            Name = rangeItem.Name;
            Description = rangeItem.Description;
        }

        public RangeFilterValueModel(T value, ParametricValueModel parametricValue) 
        {
            From = value;
            To = value;
            Name = parametricValue.Name;
            Description = parametricValue.Description;
            Code = parametricValue.Code;
            SortOrder = parametricValue.SortOrder;
            ImageUrl = parametricValue.ImageUrl;
        }

        public T From { get; set; }
        public T To { get; set; }

        internal void CheckRange(T value)
        {
            if (value.CompareTo(From) < 0) From = value;
            if (value.CompareTo(To) > 0) To = value;
        }

        public override bool IsValid => From.CompareTo(To) < 0;
    }
}
