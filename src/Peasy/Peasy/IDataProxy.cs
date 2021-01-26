namespace Peasy
{
    /// <summary>
    /// Represents a data abstraction for consumption by services, commands, and rules.
    /// </summary>
    public interface IDataProxy<T, TKey> : ISupportCRUD<T, TKey>,
                                           ISupportCRUDAsync<T, TKey>
    {
    }
}
