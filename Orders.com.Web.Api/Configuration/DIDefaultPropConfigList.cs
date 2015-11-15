using System.Collections.Generic;
using System.Configuration;

namespace Orders.com.Web.Api.Configuration
{
    [ConfigurationCollection(typeof(DIDefaultPropConfig))]
    public class DIDefaultPropConfigList : ConfigurationElementCollection, IEnumerable<DIDefaultPropConfig>
    {
        /// <summary>
        /// Gets the <see cref="DIDefaultPropConfig"/> configuration element at the specified index.
        /// </summary>
        /// <returns>The specified <see cref="DIDefaultPropConfig"/> configuration element.</returns>
        /// <param name="index">The index to retrieve.</param>
        public DIDefaultPropConfig this[int index]
        {
            get { return (DIDefaultPropConfig)BaseGet(index); }
        }

        /// <summary>
        /// Adds the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Add(DIDefaultPropConfig entry)
        {
            this.BaseAdd(entry);
        }

        /// <summary>
        /// Creates a new <see cref="DIDefaultPropConfig"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="DIDefaultPropConfig"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new DIDefaultPropConfig();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="FileDestinationConfig"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.String"/> that acts as the key for the specified <see cref="FileDestinationConfig"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DIDefaultPropConfig)element).PropertyName;
        }

        public IEnumerable<DIDefaultPropConfig> GetAll()
        {
            for (int i = 0; i < this.Count; i++)
                yield return this[i];
        }

        public new IEnumerator<DIDefaultPropConfig> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }
    }
}
