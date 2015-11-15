using System.Configuration;

namespace Orders.com.Web.Api.Configuration
{
    public class DIConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("bindings")]
        public DIConfigList Bindings
        {
            get { return (DIConfigList)base["bindings"]; }
        }
    }
}
