using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Interfaces
{
    /// <summary>
    /// The service for accessing and performing operations on the movies.
    /// </summary>
    public interface IMovieRepository
    {
        /// <summary>
        /// Gets a movie from the database with the given title.
        /// </summary>
        /// <param name="title">The title of the movie to find.</param>
        /// <returns>The movie; or null if it cannot be found.</returns>
        public Movie GetMovie(string title);

        /// <summary>
        /// Returns a list of movies that match the genre.
        /// </summary>
        /// <param name="genre">The genre to match.</param>
        /// <returns>The list of matching movies.</returns>
        public IQueryable<Movie> GetMoviesByGenre(string genre);

        /// <summary>
        /// Gets a list of all movies.
        /// </summary>
        /// <returns>The movies.</returns>
        public IQueryable<Movie> GetMovies();

        /// <summary>
        /// Gets a list of all genres.
        /// </summary>
        /// <returns>The genres.</returns>
        public IQueryable<Genre> GetGenres();

    }
}
