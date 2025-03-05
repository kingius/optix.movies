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
    /// An API to access movies by genre.
    /// </summary>

    //Authorisation is out of scope for this test.
    //[Authorize]

    [ApiController]
    [Route("[controller]")]
    public class GenresController : Controller
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<GenresController> Logger;

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
        public GenresController(IMovieRepository movieRepository, ILogger<GenresController> logger)
        {
            MovieRepository = movieRepository;
            Logger = logger;
        }

        /// <summary>
        /// Get all genres.
        /// </summary>
        /// <returns>The genre list.</returns>
        [HttpGet]
        public List<Genre> Get()
        {
            Logger.LogInformation($"Get");

            //Authorisation is out of scope for this test.
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            return MovieRepository.GetGenres()
                .ToList();
        }
    }
}
