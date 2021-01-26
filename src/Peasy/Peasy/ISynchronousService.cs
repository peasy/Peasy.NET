using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// </summary>
    public interface ISynchronousService<T, TKey> :
        ISupportSynchronousGetAllCommand<T>,
        ISupportSynchronousGetByIDCommand<T, TKey>,
        ISupportSynchronousInsertCommand<T>,
        ISupportSynchronousUpdateCommand<T>,
        ISupportSynchronousDeleteCommand<TKey>
    {
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousGetAllCommand<T>
    {
        /// <summary>
        /// </summary>
        ISynchronousCommand<IEnumerable<T>> GetAllCommand();
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousGetByIDCommand<T, TKey>
    {
        /// <summary>
        /// </summary>
        ISynchronousCommand<T> GetByIDCommand(TKey id);
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousInsertCommand<T>
    {
        /// <summary>
        /// </summary>
        ISynchronousCommand<T> InsertCommand(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousUpdateCommand<T>
    {
        /// <summary>
        /// </summary>
        ISynchronousCommand<T> UpdateCommand(T entity);
    }

    /// <summary>
    /// </summary>
    public interface ISupportSynchronousDeleteCommand<TKey>
    {
        /// <summary>
        /// </summary>
        ISynchronousCommand DeleteCommand(TKey id);
    }
}
