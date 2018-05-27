using System.Collections.Generic;

namespace Peasy
{
    public interface ISupportCRUD<T, TKey> :
        ISupportGetAll<T>,
        ISupportGetByID<T, TKey>,
        ISupportInsert<T>,
        ISupportUpdate<T>,
        ISupportDelete<TKey>
    {
    }

    public interface ISupportGetAll<T>
    {
        IEnumerable<T> GetAll();
    }

    public interface ISupportGetByID<T, TKey>
    {
        T GetByID(TKey id);
    }

    public interface ISupportInsert<T>
    {
        T Insert(T entity);
    }

    public interface ISupportUpdate<T>
    {
        T Update(T entity);
    }

    public interface ISupportDelete<TKey>
    {
        void Delete(TKey id);
    }
}
