using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Optix.Movies.Api.Models;
using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Optix.Movies.Api.Controllers
{
    /// <summary>
    /// API access to the movies.
    /// </summary>

    //Authorisation is out of scope for this test.
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<MoviesController> Logger;
        
        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        /// <summary>
        /// The movie repository.
        /// </summary>
        private IMovieRepository MovieRepository { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="movieRepository">The movie repository dependency.</param>
        /// <param name="logger">The Logger dependency.</param>
        public MoviesController(IMovieRepository movieRepository, ILogger<MoviesController> logger)
        {
            MovieRepository = movieRepository;
            Logger = logger;
        }

        /// <summary>
        /// Get all movies.
        /// </summary>
        /// <param name="page">Where to start from; defaults to 1.</param>
        /// <param name="pageSize">How many records per page; defaults to 10.</param>
        /// <returns>List of matching movies.</returns>
        [HttpGet]
        public PagedResults<Movie> Get(int page = 1, int pageSize = 10)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageSize > 1000) throw new ArgumentOutOfRangeException(nameof(pageSize));

            Logger.LogInformation($"Get");

            //Authorisation is out of scope for this test.
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var movieCount = MovieRepository.GetMovies().Count();

            return new PagedResults<Movie>()
            {
                CurrentPage = page,
                TotalPages = movieCount / pageSize + 1,
                Items = MovieRepository.GetMovies()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
            };
        }

    }
}
