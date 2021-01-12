using System;

namespace Peasy.Attributes
{
    /// <summary>
    /// Allows a way to display a type name with a more readable name
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PeasyDisplayNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the Peasy.Attributes.PeasyDisplayNameAttribute class with a specified display name.
        /// </summary>
        /// <param name="displayName">The value used to set <see cref="DisplayName"/></param>
        public PeasyDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets a value that is used for display in the UI or in error messages.
        /// </summary>
        /// <returns>
        /// A value that is used for display in the UI or in error messages.
        /// </returns>
        public string DisplayName { get; private set; }
    }
}
