using System.Collections.Generic;
using System.Configuration;

namespace Orders.com.Web.Api.Configuration
{
    public class DIConfigList : ConfigurationElementCollection, IEnumerable<DIConfig>
    {
        #region ConfigurationElementCollection
        protected override ConfigurationElement CreateNewElement()
        {
            return new DIConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DIConfig)element).FromType;
        }
        #endregion

        #region IEnumerable<DIConfig>
        public new IEnumerator<DIConfig> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }
        #endregion        

        public DIConfig this[int index]
        {
            get { return (DIConfig)BaseGet(index); }
        }

        new public DIConfig this[string name]
        {
            get { return (DIConfig)BaseGet(name); }
        }

        public void Add(DIConfig entry)
        {
            this.BaseAdd(entry);
        }

        public IEnumerable<DIConfig> GetAll()
        {
            for (int i = 0; i < this.Count; i++)
                yield return this[i];
        }
    }
}
