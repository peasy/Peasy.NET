namespace Peasy.Core
{
    public interface IDomainObject { }

    public interface IDomainObject<TKey> : IDomainObject
    {
        TKey ID { get; set; }
    }
}
