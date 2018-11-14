/// <copyright file="Repository.cs" company="Door Step Schools">
/// Copyright (c) All Right Reserved
/// </copyright>
/// <author>Ajay Gupta</author>
/// <summary>Repository class</summary>

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LibraryManagement.Data.DataContext;
using LibraryManagement.Data.Interface;
using LibraryManagement.ObjectModel;

namespace LibraryManagement.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, BaseEntity
    {
        private IDbContext _context;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        private IDbSet<TEntity> DbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public TEntity GetById(int id)
        {
            return DbSet.Single(e => e.Id.Equals(id));
        }

        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }        
    }
}
