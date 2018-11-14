/// <copyright file="IRepository.cs" company="Door Step Schools">
/// Copyright (c) All Right Reserved
/// </copyright>
/// <author>Ajay Gupta</author>
/// <summary>IRepository interface</summary>

using System;
using System.Linq;
using System.Linq.Expressions;
using LibraryManagement.ObjectModel;

namespace LibraryManagement.Data.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        void Add(TEntity entity);
    }
}
