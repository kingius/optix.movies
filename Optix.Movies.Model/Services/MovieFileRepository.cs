using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Services
{
    /// <inheritdoc/>
    public class MovieFileRepository : IMovieRepository
    {
        /// <summary>
        /// The data source for this repository.
        /// </summary>
        private IMovieDataSource DataSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataSource">The datasource.</param>
        public MovieFileRepository(IMovieDataSource dataSource)
        {
            Debug.WriteLine("MovieRepository construct");
            DataSource = dataSource;
        }

        /// <inheritdoc/>
        public Movie GetMovie(string title)
        {
            if (title is null) throw new ArgumentNullException(nameof(title));

            title = title.Trim().ToLower();

            return DataSource
                .Movies
                .FirstOrDefault(m => m.Title.ToLower() == title);
        }

        /// <inheritdoc/>
        public IQueryable<Movie> GetMoviesByGenre(string genre)
        {
            if (genre is null) throw new ArgumentNullException(nameof(genre));
            genre = genre.Trim().ToLower();

            return DataSource
                .Movies
                .Where(m => m.Genres.Select(g=>g.Name).Contains(genre))
                .ToList()
                .AsQueryable();
        }

        /// <inheritdoc/>
        public IQueryable<Genre> GetGenres()
        {
            return DataSource.Genres
                .AsQueryable();
        }

        /// <inheritdoc/>
        public IQueryable<Movie> GetMovies()
        {
            return DataSource.Movies
                .AsQueryable();
        }
    }
}
