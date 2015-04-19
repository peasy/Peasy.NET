using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile.Core 
{
    public interface ISupportCRUDAsync<T, TKey> : ISupportGetAllAsync<T>,
                                   ISupportGetByIDAsync<T, TKey>,
                                   ISupportInsertAsync<T>,
                                   ISupportUpdateAsync<T>,
                                   ISupportDeleteAsync<TKey>
    {
    }

    public interface ISupportGetAllAsync<T> 
    {
        Task<IEnumerable<T>> GetAllAsync();
    }

    public interface ISupportGetByIDAsync<T, TKey>
    {
        Task<T> GetByIDAsync(TKey id);
    }
    
    public interface ISupportInsertAsync<T> 
    {
        Task<T> InsertAsync(T entity);
    }

    public interface ISupportUpdateAsync<T> 
    {
        /// <summary>
        /// Performs an update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="FACConcurrencyException"
        Task<T> UpdateAsync(T entity);
    }

    public interface ISupportDeleteAsync<TKey>
    {
        Task DeleteAsync(TKey id);
    }
}
