using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    /// Get all movies in a particular genre.
    /// </summary>

    //Authorisation is out of scope for this test.
    //[Authorize]

    [ApiController]
    [Route("[controller]")]
    public class MoviesByGenreController : Controller
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<MoviesByGenreController> Logger;

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
        public MoviesByGenreController(IMovieRepository movieRepository, ILogger<MoviesByGenreController> logger)
        {
            MovieRepository = movieRepository;
            Logger = logger;
        }

        /// <summary>
        /// Get all movies by matching genre.
        /// </summary>
        /// <param name="genre">The genre to find.</param>
        /// <param name="page">Where to start from; defaults to 1.</param>
        /// <param name="pageSize">How many records per page; defaults to 10. Maximum 1000.</param>
        /// <returns>The matching movies.</returns>
        [HttpGet]
        public PagedResults<Movie> Get(string genre, int page = 1, int pageSize = 10)
        {
            if (genre is null) throw new ArgumentNullException(nameof(genre));
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageSize > 1000) throw new ArgumentOutOfRangeException(nameof(pageSize));

            Logger.LogInformation($"GetByGenre {genre}");

            //Authorisation is out of scope for this test.
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var matches = MovieRepository.GetMoviesByGenre(genre);
            var movieCount = matches.Count();

            return new PagedResults<Movie>()
            {
                CurrentPage = 1,
                TotalPages = movieCount / pageSize + 1,
                Items = matches
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
            };
        }
    }
}
