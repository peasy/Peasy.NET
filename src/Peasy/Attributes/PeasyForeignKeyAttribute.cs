using System;

namespace Peasy.Attributes
{
    /// <summary>
    /// Attribute used to determine whether a property of type <see cref="Nullable"/>(<see cref="Int32"/>) will participate in a <see cref="Peasy.Extensions.DomainObjectExtensions.RevertForeignKeysFromZeroToNull"/> operation.
    /// </summary>
    public class PeasyForeignKeyAttribute : Attribute
    {
    }
}
