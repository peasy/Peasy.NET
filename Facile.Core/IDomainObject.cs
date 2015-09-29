namespace Facile.Core
{
    public interface IDomainObject { }

    public interface IDomainObject<TKey> : IDomainObject
    {
        TKey ID { get; set; }
    }
}
