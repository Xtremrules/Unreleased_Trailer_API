using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using Trailer_NET_DL.Concrete;
using Trailer_NET_Library.Entities;

namespace Trailer_NET_API.Controllers
{
    [RoutePrefix("api/movies")]
    public class moviesController : ApiController
    {
        AppDbContext _db = new AppDbContext();

        [Route("all-with-released-movies"), HttpGet]
        // GET: api/Movies including released
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await  _db.Movie.ToListAsync();
        }

        // GET: api/Movies
        public async Task<IEnumerable<Movie>> Get()
        {
            var query = "SELECT TOP 10 * FROM Movie WHERE @P0 > Release_Date";
            return await _db.Database.SqlQuery<Movie>(query, DateTime.Now).ToListAsync();
        }


        // GET: api/Movies/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var query = "SELECT TOP 1 * FROM Movie WHERE ID = @p0";
            return Ok(await _db.Movie.SqlQuery(query, id).FirstOrDefaultAsync());
        }

        // POST: api/Movies
        public async Task<IHttpActionResult> Post([FromBody]Movie model)
        {
            if (ModelState.IsValid)
            {
                model.Created_Date = DateTime.Now;
                _db.Movie.Add(model);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Movies/5
        public IHttpActionResult Put(int id, [FromBody]Movie model)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(model).State = EntityState.Modified;
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Movies/5
        public async Task Delete(int id)
        {
            var query = "DELETE FROM Movie WHERE ID = @p0";
            try
            {
                await _db.Database.ExecuteSqlCommandAsync(query, id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
