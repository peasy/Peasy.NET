using System.Collections.Generic;

namespace Peasy.Synchronous
{
    /// <summary>
    /// Serves as an abstraction for business services and represents an <see cref="ISynchronousCommand"/> factory.
    /// </summary>
    /// <typeparam name="T">Represents a domain object or resource and can be any type.</typeparam>
    /// <typeparam name="TKey">Represents an identifier for a domain object or resource and can be any type.</typeparam>
    public interface ISynchronousService<T, TKey> :
        ISupportSynchronousGetAllCommand<T>,
        ISupportSynchronousGetByIDCommand<T, TKey>,
        ISupportSynchronousInsertCommand<T>,
        ISupportSynchronousUpdateCommand<T>,
        ISupportSynchronousDeleteCommand<TKey>
    {
    }

    /// <summary>
    /// Serves as an abstraction for composing a GetAllCommand.
    /// </summary>
    public interface ISupportSynchronousGetAllCommand<T>
    {
        /// <summary>
        /// Composes a command that retrieves all domain objects or resources from a source when invoked.
        /// </summary>
        /// <returns>An executable command.</returns>
        ISynchronousCommand<IEnumerable<T>> GetAllCommand();
    }

    /// <summary>
    /// Serves as an abstraction for composing a GetByIDCommand.
    /// </summary>
    public interface ISupportSynchronousGetByIDCommand<T, TKey>
    {
        /// <summary>
        /// Composes a command that retrieves a domain object or resource from a source specified by an identifier when invoked.
        /// </summary>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <returns>An executable command.</returns>
        ISynchronousCommand<T> GetByIDCommand(TKey id);
    }

    /// <summary>
    /// Serves as an abstraction for composing an InsertCommand.
    /// </summary>
    public interface ISupportSynchronousInsertCommand<T>
    {
        /// <summary>
        /// Composes a command that creates a new domain object or resource when invoked.
        /// </summary>
        /// <param name="resource">The resource to insert.</param>
        /// <returns>An executable command.</returns>
        ISynchronousCommand<T> InsertCommand(T resource);
    }

    /// <summary>
    /// Serves as an abstraction for composing an UpdateCommand.
    /// </summary>
    public interface ISupportSynchronousUpdateCommand<T>
    {
        /// <summary>
        /// Composes a command that updates an existing domain object or resource when invoked.
        /// </summary>
        /// <param name="resource">The resource to update.</param>
        /// <returns>An executable command.</returns>
        ISynchronousCommand<T> UpdateCommand(T resource);
    }

    /// <summary>
    /// Serves as an abstraction for composing a DeleteCommand.
    /// </summary>
    public interface ISupportSynchronousDeleteCommand<TKey>
    {
        /// <summary>
        /// Composes a command that deletes a domain object or resource from a source specified by an identifier when invoked.
        /// </summary>
        /// <param name="id">The id of the resource to delete.</param>
        /// <returns>An executable command.</returns>
        ISynchronousCommand DeleteCommand(TKey id);
    }
}
