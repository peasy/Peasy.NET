using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface ISupportCRUD<T, TKey> :
        ISupportGetAll<T>,
        ISupportGetByID<T, TKey>,
        ISupportInsert<T>,
        ISupportUpdate<T>,
        ISupportDelete<TKey>
    {
    }

    /// <summary>
    /// </summary>
    public interface ISupportGetAll<T>
    {
        /// <summary>
        /// </summary>
        IEnumerable<T> GetAll();
    }

    /// <summary>
    /// </summary>
    public interface ISupportGetByID<T, TKey>
    {
        /// <summary>
        /// </summary>
        T GetByID(TKey id);
    }

    /// <summary>
    /// </summary>
    public interface ISupportInsert<T>
    {
        /// <summary>
        /// </summary>
        T Insert(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportUpdate<T>
    {
        /// <summary>
        /// </summary>
        T Update(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportDelete<TKey>
    {
        /// <summary>
        /// </summary>
        void Delete(TKey id);
    }
}
