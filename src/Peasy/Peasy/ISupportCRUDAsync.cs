using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peasy
{
    /// <inheritdoc cref="IDataProxy{T, TKey}"/>
    public interface ISupportCRUDAsync<T, TKey> :
                                   ISupportGetAllAsync<T>,
                                   ISupportGetByIDAsync<T, TKey>,
                                   ISupportInsertAsync<T>,
                                   ISupportUpdateAsync<T>,
                                   ISupportDeleteAsync<TKey>
    {
    }

    /// <summary>
    /// Represents a data abstraction that asynchronously retrieves all domain objects or resources from a source.
    /// </summary>
    public interface ISupportGetAllAsync<T>
    {
        /// <summary>
        /// Retrieves all domain objects or resources from a source.
        /// </summary>
        /// <returns>An awaitable list of resources.</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }

    /// <summary>
    /// Represents a data abstraction that asynchronously retrieves a domain object or resource from a source specified by an identifier.
    /// </summary>
    public interface ISupportGetByIDAsync<T, TKey>
    {
        /// <summary>
        /// Retrieves a domain object or resource from a source specified by an identifier.
        /// </summary>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <returns>An awaitable resource.</returns>
        Task<T> GetByIDAsync(TKey id);
    }

    /// <summary>
    /// Represents a data abstraction that asynchronously creates a new domain object or resource.
    /// </summary>
    public interface ISupportInsertAsync<T>
    {
        /// <summary>
        /// Creates a new domain object or resource.
        /// </summary>
        /// <param name="resource">The resource to insert.</param>
        /// <returns>
        /// An awaitable updated representation of the created domain object or resource.
        /// </returns>
        Task<T> InsertAsync(T resource);
    }

    /// <summary>
    /// Represents a data abstraction that asynchronously updates an existing domain object or resource.
    /// </summary>
    public interface ISupportUpdateAsync<T>
    {
        /// <summary>
        /// Updates an existing domain object or resource.
        /// </summary>
        /// <param name="resource">The resource to update.</param>
        /// <returns>
        /// An awaitable updated representation of the updated domain object or resource.
        /// </returns>
        Task<T> UpdateAsync(T resource);
    }

    /// <summary>
    /// Represents a data abstraction that asynchronously deletes a domain object or resource from a source specified by an identifier.
    /// </summary>
    public interface ISupportDeleteAsync<TKey>
    {
        /// <summary>
        /// Deletes a domain object or resource from a source specified by an identifier.
        /// </summary>
        /// <param name="id">The id of the resource to delete.</param>
        /// <returns>An awaitable task.</returns>
        Task DeleteAsync(TKey id);
    }
}
