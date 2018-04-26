using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Trailer_NET_DL.Infrastructure;
using Trailer_NET_Library.Abstract;
using Trailer_NET_Library.Entities;

namespace Trailer_NET_API.Controllers
{
    [RoutePrefix("api/movies")]
    public class moviesController : ApiController
    {
        private readonly IEntityBaseRepository<Movie> _movieRepo;
        private IUnitOfWork _unitOfWork;

        public moviesController(IEntityBaseRepository<Movie> _movieRepo, IUnitOfWork _unitOfWork)
        {
            this._movieRepo = _movieRepo;
            this._unitOfWork = _unitOfWork;
        }

        [Route("all"), HttpGet]
        // GET: api/Movies including released
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await  _movieRepo.AllAsync();
        }

        // GET: api/Movies
        public async Task<IEnumerable<Movie>> Get()
        {
            return await _movieRepo.AllIncludingAsync(x => DateTime.Now > x.Release_Date);
        }

        public async Task<IEnumerable<Movie>> Get10()
        {

        }

        // GET: api/Movies/5
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return Ok(await _movieRepo.GetByIdAsync(id));

        }

        // POST: api/Movies
        public async Task<IHttpActionResult> Post([FromBody]Movie model)
        {
            if (ModelState.IsValid)
            {
                model.Created_Date = DateTime.Now;
                _movieRepo.Create(model);
                var m = await _unitOfWork.SaveChangesAsync();
                return Ok(m);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Movies/5
        public async Task<IHttpActionResult> Put(Guid id, [FromBody]Movie model)
        {
            if (ModelState.IsValid)
            {
                _movieRepo.Update(model);
                var v = await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Movies/5
        public async Task Delete(Guid id)
        {
            var m = await _movieRepo.GetByIdAsync(id);
            _movieRepo.Delete(m);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
