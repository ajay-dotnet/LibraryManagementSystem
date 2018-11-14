using System;
using System.Configuration;
using System.Data.Services.Client;
using LibraryManagement.LibraryClient;

namespace LibraryManagementUI.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        public BaseRepository()
        {
            ServiceUrl = new Uri(ConfigurationManager.AppSettings["LibraryClientUrl"]);
            Container = new Container(ServiceUrl);

            Container.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            //this is set to fix the web api timeout issue, for processes that take longer than the default 1 minute
            Container.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["WebApiTimeoutDuration"]);
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public Container Container { get; private set; }

        /// <summary>
        /// Gets the service URL.
        /// </summary>
        /// <value>
        /// The service URL.
        /// </value>
        public Uri ServiceUrl { get; private set; }

        /// <summary>
        /// Creates the specified entity set name.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="entitySetName">Name of the entity set.</param>
        /// <param name="entity">The entity.</param>
        public void Create<T>(string entitySetName, T entity)
        {
            this.Container.AddObject(entitySetName, entity);
            this.Container.SaveChanges();
        }

        public void CreateMultiple<T>(string entitySetName, T entity)
        {
            this.Container.AddObject(entitySetName, entity);
            this.Container.SaveChanges();
            this.Container.Detach(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="entitySetName">Name of the entity set.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="attach">attach true attach context else.</param>
        public void Update<T>(string entitySetName, T entity, bool attach = true)
        {
            if (attach)
            {
                this.Container.AttachTo(entitySetName, entity);
            }

            this.Container.UpdateObject(entity);
            this.Container.SaveChanges(SaveChangesOptions.PatchOnUpdate);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="entitySetName">Name of the entity set.</param>
        /// <param name="entity">The entity.</param>
        public void Delete<T>(string entitySetName, T entity)
        {
            this.Container.AttachTo(entitySetName, entity);
            this.Container.DeleteObject(entity);
            this.Container.SaveChanges();
        }

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Detach(object entity)
        {
            return Container.Detach(entity);
        }
    }
}