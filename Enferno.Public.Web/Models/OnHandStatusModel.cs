
using System;

namespace Enferno.Public.Web.Models
{
    /// <summary>
    /// OnHandStatus is shown on the site. Should be set in SiteRules and displayed on product pages, lists och in the basket.
    /// </summary>
    [Serializable]
    public class OnHandStatusModel : IComparable<OnHandStatusModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public OnHandStatus Status { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        
        /// <summary>
        /// Count can be set to any value and will be used when getting the best onhand from lists.
        /// </summary>
        public decimal Count { get; set; }

        public int CompareTo(OnHandStatusModel other)
        {
            return Status != other.Status ? Status.CompareTo(other.Status) : Count.CompareTo(other.Count);
        }
    }
}
