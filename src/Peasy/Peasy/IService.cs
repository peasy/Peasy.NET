using System.Collections.Generic;

namespace Peasy
{
    /// <summary>
    /// Serves as an abstraction for business services and represents an <see cref="ICommand"/> factory.
    /// </summary>
    /// <typeparam name="T">Represents a domain object or resource and can be any type.</typeparam>
    /// <typeparam name="TKey">Represents an identifier for a domain object or resource and can be any type.</typeparam>
    public interface IService<T, TKey> :
        ISupportGetAllCommand<T>,
        ISupportGetByIDCommand<T, TKey>,
        ISupportInsertCommand<T>,
        ISupportUpdateCommand<T>,
        ISupportDeleteCommand<TKey>
    {
    }

    /// <summary>
    /// Serves as an abstraction for composing a GetAllCommand.
    /// </summary>
    public interface ISupportGetAllCommand<T>
    {
        /// <summary>
        /// Composes a command that retrieves all domain objects or resources from a source when invoked.
        /// </summary>
        /// <returns>An executable command.</returns>
        ICommand<IEnumerable<T>> GetAllCommand();
    }

    /// <summary>
    /// Serves as an abstraction for composing a GetByIDCommand.
    /// </summary>
    public interface ISupportGetByIDCommand<T, TKey>
    {
        /// <summary>
        /// Composes a command that retrieves a domain object or resource from a source specified by an identifier when invoked.
        /// </summary>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <returns>An executable command.</returns>
        ICommand<T> GetByIDCommand(TKey id);
    }

    /// <summary>
    /// Serves as an abstraction for composing an InsertCommand.
    /// </summary>
    public interface ISupportInsertCommand<T>
    {
        /// <summary>
        /// Composes a command that creates a new domain object or resource when invoked.
        /// </summary>
        /// <param name="resource">The resource to insert.</param>
        /// <returns>An executable command.</returns>
        ICommand<T> InsertCommand(T resource);
    }

    /// <summary>
    /// Serves as an abstraction for composing an UpdateCommand.
    /// </summary>
    public interface ISupportUpdateCommand<T>
    {
        /// <summary>
        /// Composes a command that updates an existing domain object or resource when invoked.
        /// </summary>
        /// <param name="resource">The resource to update.</param>
        /// <returns>An executable command.</returns>
        ICommand<T> UpdateCommand(T resource);
    }

    /// <summary>
    /// Serves as an abstraction for composing a DeleteCommand.
    /// </summary>
    public interface ISupportDeleteCommand<TKey>
    {
        /// <summary>
        /// Composes a command that deletes a domain object or resource from a source specified by an identifier when invoked.
        /// </summary>
        /// <param name="id">The id of the resource to delete.</param>
        /// <returns>An executable command.</returns>
        ICommand DeleteCommand(TKey id);
    }
}
