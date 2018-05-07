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

        public async Task<IEnumerable<string>> get(int id)
        {
            var url = Url.Content("~/images/");
            //var query = "SELECT @p0 + File_Name FROM Image WHERE MovieID = @p1";
            var query = "SELECT @p0 + File_Name FROM Images WHERE ID IN " +
                "(Select ImageID From Movie_Image Where MovieID = @p1)";
            return await _db.Database.SqlQuery<string>(query, url, id).ToListAsync();
        }
    }
}
