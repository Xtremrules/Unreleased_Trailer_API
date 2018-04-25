using Trailer_NET_DL.Concrete;

namespace Trailer_NET_DL.Infrastructure
{
    public interface IDbFactory
    {
        AppDbContext Initialize();
    }
}
