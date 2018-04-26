using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Trailer_NET_DL.Concrete;
using Trailer_NET_Library.Entities;

namespace Trailer_NET_API.Controllers
{
    [RoutePrefix("api/images")]
    public class imagesController : ApiController
    {
        AppDbContext _db = new AppDbContext();

        public async Task<IEnumerable<Image>> get(int id)
        {
            var query = "SELECT * FROM Image WHERE MovieID = @p0";
            return await _db.Image.SqlQuery(query, id).ToListAsync();
        }
    }
}
