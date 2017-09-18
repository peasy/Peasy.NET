using System.Collections.Generic;

namespace Peasy
{
    public interface IService
    {
    }
    public interface IService<T, TKey> : ISupportGetAllCommand<T>, 
                                         ISupportGetByIDCommand<T, TKey>,
                                         ISupportInsertCommand<T>,
                                         ISupportUpdateCommand<T>,
                                         ISupportDeleteCommand<TKey>
    {
    }

    public interface ISupportGetAllCommand<T> : IService
    {
        ICommand<IEnumerable<T>> GetAllCommand();
    }

    public interface ISupportGetByIDCommand<T, TKey> : IService
    {
        ICommand<T> GetByIDCommand(TKey id);
    }

    public interface ISupportInsertCommand<T> : IService
    {
        ICommand<T> InsertCommand(T entity);
    }

    public interface ISupportUpdateCommand<T> : IService
    {
        ICommand<T> UpdateCommand(T entity);
    }

    public interface ISupportDeleteCommand<TKey> : IService
    {
        ICommand DeleteCommand(TKey id);
    }
}
