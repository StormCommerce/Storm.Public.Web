
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Enferno.Public.Web.Configuration
{
    public class NamedParametricMappingsSection : ConfigurationSection
    {
        [ConfigurationProperty("mappings")]
        [ConfigurationCollection(typeof(NamedParametricMappingElement), AddItemName = "mapping")]
        public VariantParametricMappings Mappings
        {
            get { return (VariantParametricMappings)this["mappings"]; }
            set { this["mappings"] = value; }
        }
    }

    public class NamedParametricMappingElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("parametricId", DefaultValue = 0, IsRequired = true)]
        public int ParametricId
        {
            get { return (int)this["parametricId"]; }
            set { this["parametricId"] = value; }
        }
    }

    public class VariantParametricMappings : ConfigurationElementCollection, IList<NamedParametricMappingElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new NamedParametricMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NamedParametricMappingElement) element).Name;
        }

        bool ICollection<NamedParametricMappingElement>.IsReadOnly => IsReadOnly();

        public int IndexOf(NamedParametricMappingElement item)
        {
            return BaseIndexOf(item);
        }

        public void Insert(int index, NamedParametricMappingElement item)
        {
            BaseAdd(index, item);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public NamedParametricMappingElement this[int index]
        {
            get
            {
                return (NamedParametricMappingElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(NamedParametricMappingElement item)
        {
            BaseAdd(item, true);
        }

        public void Clear()
        {
            BaseClear();
        }

        public bool Contains(NamedParametricMappingElement item)
        {
            return !(IndexOf(item) < 0);
        }

        public void CopyTo(NamedParametricMappingElement[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public new bool IsReadOnly => IsReadOnly();

        public bool Remove(NamedParametricMappingElement item)
        {
            BaseRemove(GetElementKey(item));

            return true;
        }

        public new IEnumerator<NamedParametricMappingElement> GetEnumerator()
        {
            return this.OfType<NamedParametricMappingElement>().GetEnumerator();
        }
    }
}
