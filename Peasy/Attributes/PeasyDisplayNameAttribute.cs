using System;

namespace Peasy.Attributes
{
    public class PeasyDisplayNameAttribute : Attribute
    {
        public PeasyDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }
    }
}
