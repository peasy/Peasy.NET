using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Phoebe.Core.Configuration
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
