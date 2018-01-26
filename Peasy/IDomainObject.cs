namespace Peasy
{
    public interface IDomainObject { }

    public interface IDomainObject<TKey> : IDomainObject
    {
        TKey ID { get; set; }
    }
}
