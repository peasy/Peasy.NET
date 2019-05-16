using System.Collections.Generic;

namespace Peasy
{
    public interface IService<T, TKey> : ISupportGetAllCommand<T>,
                                         ISupportGetByIDCommand<T, TKey>,
                                         ISupportInsertCommand<T>,
                                         ISupportUpdateCommand<T>,
                                         ISupportDeleteCommand<TKey>
    {
    }

    public interface ISupportGetAllCommand<T>
    {
        ICommand<IEnumerable<T>> GetAllCommand();
    }

    public interface ISupportGetByIDCommand<T, TKey>
    {
        ICommand<T> GetByIDCommand(TKey id);
    }

    public interface ISupportInsertCommand<T>
    {
        ICommand<T> InsertCommand(T entity);
    }

    public interface ISupportUpdateCommand<T>
    {
        ICommand<T> UpdateCommand(T entity);
    }

    public interface ISupportDeleteCommand<TKey>
    {
        ICommand DeleteCommand(TKey id);
    }
}
