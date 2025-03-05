using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Optix.Movies.Api.Controllers
{
    /// <summary>
    /// API access to a movie.
    /// </summary>

    //Authorisation is out of scope for this test.
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<MovieController> Logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        //static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        /// <summary>
        /// The movie repository.
        /// </summary>
        private IMovieRepository MovieRepository { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="movieRepository">The movie repository dependency.</param>
        /// <param name="logger">The Logger dependency.</param>
        public MovieController(IMovieRepository movieRepository, ILogger<MovieController> logger)
        {
            MovieRepository = movieRepository;
            Logger = logger;
        }

        /// <summary>
        /// Get a movie by title.
        /// </summary>
        /// <param name="movieName">The name of the movie to search for.</param>
        /// <returns>The matching movie; or null.</returns>
        [HttpGet]
        public Movie Get(string movieName)
        {
            if (movieName is null) throw new ArgumentNullException(nameof(movieName));

            Logger.LogInformation($"Get {movieName}");

            //Authorisation is out of scope for this test.
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            return MovieRepository.GetMovie(movieName);
        }
    }
}
