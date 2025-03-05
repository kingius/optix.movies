using Microsoft.EntityFrameworkCore;
using Optix.Movies.Model.Data;
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
    public class MovieRepository:IMovieRepository
    {
        /// <summary>
        /// The data source for this repository.
        /// </summary>
        private MovieDbContext DataSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataSource">The datasource.</param>
        public MovieRepository(MovieDbContext dataSource)
        {
            DataSource = dataSource;
        }

        /// <inheritdoc/>
        public Movie GetMovie(string title)
        {
            if (title is null) throw new ArgumentNullException(nameof(title));

            title = title.Trim();

            return DataSource
                .Movies
                .Include(m => m.Genres)
                .FirstOrDefault(m => m.Title == title);
        }

        /// <inheritdoc/>
        public IQueryable<Movie> GetMoviesByGenre(string genre)
        {
            if (genre is null) throw new ArgumentNullException(nameof(genre));

            genre = genre.Trim();

            return DataSource
                .Movies
                .Where(m => m.Genres.Select(g => g.Name).Contains(genre))
                .Include(m => m.Genres);
        }

        /// <inheritdoc/>
        public IQueryable<Genre> GetGenres()
        {
            return DataSource.Genres;
        }

        /// <inheritdoc/>
        public IQueryable<Movie> GetMovies()
        {
            return DataSource.Movies
                .Include(m => m.Genres);
        }
    }
}
