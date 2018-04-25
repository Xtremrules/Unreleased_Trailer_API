using Trailer_NET_DL.Concrete;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_DL.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        AppDbContext dbContext;

        public AppDbContext Initialize()
        {
            return dbContext ?? (dbContext = new AppDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
