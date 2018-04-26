using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Trailer_NET_Library.Abstract
{
    [Obsolete("You wont be able to access dbconxet if you use this class", false)]
    public interface IEntityBaseRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> AllAsync();
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(object Id);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
