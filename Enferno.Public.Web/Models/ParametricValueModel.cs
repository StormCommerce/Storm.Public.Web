
using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ParametricValueModel : IEquatable<ParametricValueModel>
    {
        public bool Equals(ParametricValueModel other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Description, other.Description) &&
                   string.Equals(ImageUrl, other.ImageUrl) && string.Equals(Code, other.Code) &&
                   string.Equals(Value, other.Value) && SortOrder == other.SortOrder;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ImageUrl != null ? ImageUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ SortOrder;
                return hashCode;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public int SortOrder { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParametricValueModel) obj);
        }
    }
}
