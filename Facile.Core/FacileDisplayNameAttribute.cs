using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Core
{
    public class FacileDisplayNameAttribute : Attribute
    {
        public FacileDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }
    }
}
