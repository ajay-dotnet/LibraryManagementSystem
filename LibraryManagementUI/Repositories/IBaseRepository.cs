using LibraryManagement.LibraryClient;

namespace LibraryManagementUI.Repositories
{

    /// <summary>
    /// The BaseRepository interface.
    /// </summary>
    public interface IBaseRepository
    {

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <returns>The container.</returns>
        Container Container { get; }

        /// <summary>
        /// Creates the specified entity set name.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="entitySetName">Name of the entity set.</param>
        /// <param name="entity">The entity.</param>
        void Create<T>(string entitySetName, T entity);

        void CreateMultiple<T>(string entitySetName, T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="entitySetName">Name of the entity set.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="attach">attach true attach context else.</param>
        void Update<T>(string entitySetName, T entity, bool attach = true);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="entitySetName">Name of the entity set.</param>
        /// <param name="entity">The entity.</param>
        void Delete<T>(string entitySetName, T entity);

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Detach(object entity);
    }
}