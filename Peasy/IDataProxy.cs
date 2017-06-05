namespace Peasy
{
    public interface IDataProxy<T, TKey> : ISupportCRUD<T, TKey>,
                                           ISupportCRUDAsync<T, TKey>
    {
    }
}
