using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CompanyName.ProjectName.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private readonly DbSet<T> dbSet;

        protected AppDbContext DbContext { get; set; }

        public bool LazyLoadingEnabled { get; set; }
        #endregion

        protected RepositoryBase(AppDbContext dbContext)
        {
            DbContext = dbContext;
            dbSet = DbContext.Set<T>();

            LazyLoadingEnabled = false;
            DbContext.ChangeTracker.LazyLoadingEnabled = LazyLoadingEnabled;
            DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        #region Implementation

        public virtual void Add(T entity)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            //dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            DbContext.ChangeTracker.LazyLoadingEnabled = true;

            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            DbContext.ChangeTracker.LazyLoadingEnabled = LazyLoadingEnabled;

            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            DbContext.ChangeTracker.LazyLoadingEnabled = LazyLoadingEnabled;

            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            DbContext.ChangeTracker.LazyLoadingEnabled = true;

            return dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion
    }
}
