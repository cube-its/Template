using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CompanyName.ProjectName.Domain.Contracts.Repositories
{
    /// <summary>
    /// Generic repository interface.
    /// </summary>
    /// <typeparam name="T">Generic model</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Flag to enable/disable lazy loading of the data.
        /// </summary>
        bool LazyLoadingEnabled { get; set; }

        /// <summary>
        /// Marks an entity as new.
        /// </summary>
        /// <param name="entity">Entity of generic type</param>
        void Add(T entity);

        /// <summary>
        /// Marks an entity as modified.
        /// </summary>
        /// <param name="entity">Entity of generic type</param>
        void Update(T entity);

        /// <summary>
        /// Marks an entity to be removed.
        /// </summary>
        /// <param name="entity">Entity of generic type</param>
        void Delete(T entity);

        /// <summary>
        /// Marks all entities satisfying condition to be removed.
        /// </summary>
        /// <param name="where">Expression</param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets an entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity</param>
        /// <returns>Entity</returns>
        T GetById(int id);

        /// <summary>
        /// Gets an entity using delegate.
        /// </summary>
        /// <param name="where">Expression</param>
        /// <returns>Entity</returns>
        T Get(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets all entities of type T.
        /// </summary>
        /// <returns>All entities of type T</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets entities using delegate.
        /// </summary>
        /// <param name="where">Expression</param>
        /// <returns>Entities of type T</returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
