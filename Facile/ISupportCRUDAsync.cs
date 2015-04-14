using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile 
{
    public interface ISupportCRUDAsync<T> : ISupportGetAllAsync<T>,
                                   ISupportGetByIDAsync<T>,
                                   ISupportInsertAsync<T>,
                                   ISupportUpdateAsync<T>,
                                   ISupportDeleteAsync
    {
    }

    public interface ISupportGetAllAsync<T> 
    {
        Task<IEnumerable<T>> GetAllAsync();
    }

    public interface ISupportGetByIDAsync<T>
    {
        Task<T> GetByIDAsync(int id);
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

    public interface ISupportDeleteAsync
    {
        Task DeleteAsync(int id);
    }
}
