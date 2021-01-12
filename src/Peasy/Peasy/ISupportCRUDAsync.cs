using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface ISupportCRUDAsync<T, TKey> :
                                   ISupportGetAllAsync<T>,
                                   ISupportGetByIDAsync<T, TKey>,
                                   ISupportInsertAsync<T>,
                                   ISupportUpdateAsync<T>,
                                   ISupportDeleteAsync<TKey>
    {
    }

    /// <summary>
    /// </summary>
    public interface ISupportGetAllAsync<T>
    {
        /// <summary>
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
    }

    /// <summary>
    /// </summary>
    public interface ISupportGetByIDAsync<T, TKey>
    {
        /// <summary>
        /// </summary>
        Task<T> GetByIDAsync(TKey id);
    }

    /// <summary>
    /// </summary>
    public interface ISupportInsertAsync<T>
    {
        /// <summary>
        /// </summary>
        Task<T> InsertAsync(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportUpdateAsync<T>
    {
        /// <summary>
        /// </summary>
        Task<T> UpdateAsync(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportDeleteAsync<TKey>
    {
        /// <summary>
        /// </summary>
        Task DeleteAsync(TKey id);
    }
}
