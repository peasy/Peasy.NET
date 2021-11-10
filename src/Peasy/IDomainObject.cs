namespace Peasy
{
    /// <summary>
    /// A marker interface that represents a domain object or resource.
    /// </summary>
    public interface IDomainObject { }

    /// <summary>
    /// Represents a domain object or resource.
    /// </summary>
    /// <typeparam name="TKey">Represents an identifier for a domain object or resource and can be any type.</typeparam>
    public interface IDomainObject<TKey> : IDomainObject
    {
        /// <summary>
        /// Represents the identifier of a domain object or resource.
        /// </summary>
        TKey ID { get; }
    }
}
