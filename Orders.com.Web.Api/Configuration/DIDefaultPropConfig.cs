using System.Configuration;

namespace Orders.com.Web.Api.Configuration
{
    public class DIDefaultPropConfig : ConfigurationElement
    {
        [ConfigurationProperty("propertyName")]
        public string PropertyName
        {
            get { return (string)base["propertyName"]; }
            set { base["propertyName"] = value; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
