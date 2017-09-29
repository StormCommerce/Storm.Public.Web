
using System;
using System.Collections.Generic;
using System.Linq;
using Enferno.StormApiClient.ExposeProxy;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class ParametricModel : IEquatable<ParametricModel>
    {
        public bool Equals(ParametricModel other)
        {
            var retval = Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Description, other.Description) &&
                   string.Equals(Uom, other.Uom) && IsPrimary.Equals(other.IsPrimary) && GroupId == other.GroupId &&
                   string.Equals(Group, other.Group) && ValueType == other.ValueType;
            if (!retval)
                return false;
            return (ReferenceEquals(Values, other.Values) || (Values != null && other.Values != null && Values.SequenceEqual(other.Values)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Uom != null ? Uom.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ IsPrimary.GetHashCode();
                hashCode = (hashCode*397) ^ GroupId;
                hashCode = (hashCode*397) ^ (Group != null ? Group.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) ValueType;
                //hashCode = (hashCode*397) ^ (Values != null ? Values.GetHashCode() : 0);
                return hashCode;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Unit of measurement. Should be appended to the value when displaying values.
        /// </summary>
        public string Uom { get; set; }
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Can be used to group parametrics for display. Not used in ProductItemModels.
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// Can be used to group parametrics for display. Not used in ProductItemModels.
        /// </summary>
        public string Group { get; set; }

        public ParametricValueType ValueType { get; set; }

        public List<ParametricValueModel> Values { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParametricModel) obj);
        }
    }
}
