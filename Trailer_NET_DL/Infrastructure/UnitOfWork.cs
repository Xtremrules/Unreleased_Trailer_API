using System.Threading.Tasks;
using Trailer_NET_DL.Concrete;

namespace Trailer_NET_DL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private AppDbContext dbContext;

        public AppDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Initialize()); }
        }

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
