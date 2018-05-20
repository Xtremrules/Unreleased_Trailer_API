﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Trailer_NET_API.Models;
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
            return await _db.Movie.ToListAsync();
        }

        [Route(""), HttpGet]
        // GET: api/Movies
        public async Task<IEnumerable<Movie>> Get()
        {
            var query = "SELECT TOP 10 * FROM Movies WHERE (@P0 <= Release_Date OR Release_Date IS NULL)";
            return await _db.Database.SqlQuery<Movie>(query, DateTime.Now).ToListAsync();
        }

        //[Route("/{id}"), HttpGet]
        // GET: api/Movies/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var query = "SELECT TOP 1 * FROM Movies WHERE ID = @p0";
            return Ok(await _db.Movie.SqlQuery(query, id).FirstOrDefaultAsync());
        }

        [HttpGet, Route("paging")]
        public async Task<IEnumerable<Movie>> Get([FromUri]PagingParameterModel model)
        {
            var query = "SELECT * FROM Movies WHERE (@P0 <= Release_Date OR Release_Date IS NULL)";

            // Return List of Movies  
            var source = await _db.Movie.SqlQuery(query, DateTime.Now).ToListAsync();

            // Get's No of Rows Count   
            int count = source.Count;

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = model.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = model.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }

        [HttpGet, Route("search/{q}/{key}")]
        public async Task<IEnumerable<Movie>> Get(string q, string key = null)
        {
            var query = "SELECT * FROM Movies WHERE (@P0 <= Release_Date OR Release_Date IS NULL) And Title like '%@p1%'";
            var queryKey = "SELECT * FROM Movies Where Title like '%@p0%'";

            var movies = new List<Movie>();

            if (key != null)
                movies = await _db.Movie.SqlQuery(query, DateTime.Now, q).ToListAsync();
            else
                movies = await _db.Movie.SqlQuery(queryKey, q).ToListAsync();

            return movies;
        }

        [HttpGet, Route("liked/{userid}")]
        public async Task<IEnumerable<Movie>> GetLiked(string UserID)
        {
            var query = "SELECT * FROM Movies Where ID IN " +
                "( SELECT MovieID FROM Liked_Table Where UserID = @p0 )";

            return await _db.Movie.SqlQuery(query, UserID).ToListAsync();
        }

        [HttpGet, Route("liked-number/{userid}")]
        public string GetLiked_Number(string UserID)
        {
            var query = "SELECT Count(*) FROM Movies Where ID IN " +
                "( SELECT MovieID FROM Liked_Table Where UserID = @p0 )";

            return _db.Database.SqlQuery<string>(query, UserID).ToString();
        }

        [HttpGet, Route("released-liked/{userid}")]
        public async Task<IEnumerable<Movie>> Get_Liked_released(string UserId)
        {
            var query = "SELECT * FROM Movies Where Release_Date >= @p0 AND ID IN " +
                "( SELECT MovieID FROM Liked_Table Where UserID = @p1 )";

            return await _db.Movie.SqlQuery(query, DateTime.Now, UserId).ToListAsync();
        }

        [HttpPost, Route("add-liked")]
        public IHttpActionResult Add_Liked([FromBody]AddLiked model)
        {
            var query = "Insert Into Liked_Table(UserId, MovieID) Values (@p0,@p1)";

            _db.Database.ExecuteSqlCommand(query, model.UserID, model.MovieID);
            return Ok();
        }

        [HttpPost, Route("")]
        // POST: api/Movies
        public async Task<HttpResponseMessage> Post([FromBody]Movie model)
        {
            if (ModelState.IsValid)
            {
                model.Created_Date = DateTime.Now;
                _db.Movie.Add(model);
                await _db.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet, Route("{id:int}/images")]
        public async Task<IEnumerable<string>> Images(int id)
        {
            var url = Url.Content("~/images/");
            var query = "SELECT @p0 + File_Name FROM Images WHERE ID IN " +
                "(Select ImageID From Movie_Image Where MovieID = @p1)";

            var images = await _db.Database.SqlQuery<string>(query, url, id).ToListAsync();
            return images;
        }

        [HttpPost, Route("{id:int}/upload-images")]
        public HttpResponseMessage UploadImage(int id)
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Check if file is uploaded
            if (postedFile == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Select Image");

            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);

            Image image = new Image()
            {
                File_Name = imageName,
            };

            throw new NotImplementedException();
        }

        // PUT: api/Movies/5
        //[HttpPut, Route("{id}")]
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
            var query = "DELETE FROM Movies WHERE ID = @p0";
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
