using System.Threading.Tasks;

namespace Trailer_NET_DL.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
