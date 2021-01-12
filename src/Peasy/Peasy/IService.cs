using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface IService<T, TKey> : ISupportGetAllCommand<T>,
                                         ISupportGetByIDCommand<T, TKey>,
                                         ISupportInsertCommand<T>,
                                         ISupportUpdateCommand<T>,
                                         ISupportDeleteCommand<TKey>
    {
    }

    /// <summary>
    /// </summary>
    public interface ISupportGetAllCommand<T>
    {
        /// <summary>
        /// </summary>
        ICommand<IEnumerable<T>> GetAllCommand();
    }

    /// <summary>
    /// </summary>
    public interface ISupportGetByIDCommand<T, TKey>
    {
        /// <summary>
        /// </summary>
        ICommand<T> GetByIDCommand(TKey id);
    }

    /// <summary>
    /// </summary>
    public interface ISupportInsertCommand<T>
    {
        /// <summary>
        /// </summary>
        ICommand<T> InsertCommand(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportUpdateCommand<T>
    {
        /// <summary>
        /// </summary>
        ICommand<T> UpdateCommand(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportDeleteCommand<TKey>
    {
        /// <summary>
        /// </summary>
        ICommand DeleteCommand(TKey id);
    }
}
