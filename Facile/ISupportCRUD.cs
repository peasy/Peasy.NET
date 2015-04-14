using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public interface ISupportCRUD<T> : ISupportGetAll<T>,
                                       ISupportGetByID<T>,
                                       ISupportInsert<T>,
                                       ISupportUpdate<T>,
                                       ISupportDelete
 
    {
    }

    public interface ISupportGetAll<T> 
    {
        IEnumerable<T> GetAll();
    }

    public interface ISupportGetByID<T>
    {
        T GetByID(int id);
    }

    public interface ISupportInsert<T> 
    {
        T Insert(T entity);
    }

    public interface ISupportUpdate<T> 
    {
        /// <summary>
        /// Performs an update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="FACConcurrencyException"
        T Update(T entity);
    }

    public interface ISupportDelete
    {
        void Delete(int id);
    }
}
