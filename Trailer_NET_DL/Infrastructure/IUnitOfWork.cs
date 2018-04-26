using System;
using System.Threading.Tasks;

namespace Trailer_NET_DL.Infrastructure
{
    [Obsolete("You wont be able to access dbconxet if you use this class", false)]
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
