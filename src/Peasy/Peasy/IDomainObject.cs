namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface IDomainObject { }

    /// <summary>
    /// </summary>
    public interface IDomainObject<TKey> : IDomainObject
    {
        /// <summary>
        /// </summary>
        TKey ID { get; set; }
    }
}
