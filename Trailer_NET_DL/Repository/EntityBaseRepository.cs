using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trailer_NET_DL.Concrete;
using Trailer_NET_DL.Infrastructure;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_DL.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : BaseEntity
    {
        protected AppDbContext _dbContext;

        #region Properties
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected AppDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Initialize()); }
            private set
            {
                _dbContext = value;
            }
        }

        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion

        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> AllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Asynchronously finds an entity with the given primary key values. If an entity
        /// with the given primary key values exists in the context, then it is returned
        /// immediately without making a request to the store. Otherwise, a request is made
        /// to the store for an entity with the given primary key values and this entity,
        /// if found, is attached to the context and returned. If no entity is found in the
        /// context or the store, then null is returned.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(object Id)
        {
            var entity = await DbContext.Set<T>().FindAsync(Id);
            if (entity == null) return default(T); else return entity;
        }

        public void Create(T entity)
        {
            if (entity == null) throwError();
            DbContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            if (entity == null) throwError();
            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            if (entity == null) throwError();
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        void throwError()
        {
            throw new ArgumentNullException("entity", "can't pass a null object to this method");
        }
    }
}
