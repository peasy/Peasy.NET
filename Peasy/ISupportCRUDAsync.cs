using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Peasy
{
    public interface ISupportCRUDAsync<T, TKey> :
                                   ISupportGetAllAsync<T>,
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
        Task<T> UpdateAsync(T entity);
    }

    public interface ISupportDeleteAsync<TKey>
    {
        Task DeleteAsync(TKey id);
    }

    public interface ISupportGetByFilterAsync<T>
    {
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> predicate);
    }
}
