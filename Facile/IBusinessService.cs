using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facile
{
    public interface IBusinessService<T>
    {
        ICommand<IEnumerable<T>> GetAllCommand();
        ICommand<T> GetByIDCommand(int id);
        ICommand<T> InsertCommand(T entity);
        ICommand<T> UpdateCommand(T entity);
        ICommand DeleteCommand(int id);
    }
}
