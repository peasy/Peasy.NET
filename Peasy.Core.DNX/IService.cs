using System.Collections.Generic;

namespace Peasy.Core
{
    public interface IService<T, TKey>
    {
        ICommand<IEnumerable<T>> GetAllCommand();
        ICommand<T> GetByIDCommand(TKey id);
        ICommand<T> InsertCommand(T entity);
        ICommand<T> UpdateCommand(T entity);
        ICommand DeleteCommand(TKey id);
    }
}
