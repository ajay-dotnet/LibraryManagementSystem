/// <copyright file="LibraryManagementDbContext.cs" company="Door Step Schools">
/// Copyright (c) All Right Reserved
/// </copyright>
/// <author>Ajay Gupta</author>
/// <summary>DbContext class</summary>

using System.Data.Entity;
using LibraryManagement.ObjectModel;

namespace LibraryManagement.Data.DataContext
{
    public class LibraryManagementDbContext : DbContext, IDbContext
    {
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleCategory> TitleCategories { get; set; }
        public DbSet<Fairy> Fairies { get; set; }
        public DbSet<Level> Levels { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LibraryManagementDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public System.Data.Entity.DbSet<LibraryManagement.ObjectModel.Book> Books { get; set; }

        public System.Data.Entity.DbSet<LibraryManagement.ObjectModel.BookCondition> BookConditions { get; set; }

        public System.Data.Entity.DbSet<LibraryManagement.ObjectModel.TitleList> TitleLists { get; set; }

        public System.Data.Entity.DbSet<LibraryManagement.ObjectModel.CheckInRecord> CheckInRecords { get; set; }
    }

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}