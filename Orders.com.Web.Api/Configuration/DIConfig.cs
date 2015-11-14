using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Phoebe.Core.Configuration
{
    public class DIConfig : ConfigurationSection
    {
        [ConfigurationProperty("fromType", IsKey=true)]
        public string FromType
        {
            get { return (string)base["fromType"]; }
            set { base["fromType"] = value; }
        }

        [ConfigurationProperty("toType")]
        public string ToType
        {
            get { return (string)base["toType"]; }
            set { base["toType"] = value; }
        }

        [ConfigurationProperty("defaultProperties")]
        public DIDefaultPropConfigList DefaultProperties
        {
            get { return (DIDefaultPropConfigList)base["defaultProperties"]; }
            set { base["defaultProperties"] = value; }
        }
    }
}
