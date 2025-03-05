using Moq;
using NUnit.Framework;
using Optix.Movies.Model.Data;
using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Test.Services
{
    class MovieFileRepositoryTests
    {
        /// <summary>
        /// Dependency of the movie respository.
        /// </summary>
        public MovieFileDataSource DataSource { get; private set; }

        /// <summary>
        /// The movie repository.
        /// </summary>
        public IMovieRepository MovieRepository { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            DataSource = new MovieFileDataSource();
            MovieRepository = new MovieFileRepository(DataSource);
        }

        [Test]
        public void GetMovieReturnsMatchingMovie()
        {
            var title = "Pixie Hollow Bake Off";
            var result = MovieRepository.GetMovie(title);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(title));
        }

        [Test]
        public void GetMovieReturnsMovieWithCaseInsenstiveSearch()
        {
            var title = "PIXIE hollow BaKe oFF";
            var result = MovieRepository.GetMovie(title);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title.ToUpper(), Is.EqualTo(title.ToUpper())); //case insensitive match
        }

        [Test]
        public void GetMovieReturnsTrimmedMovieWithTrimmedSearch()
        {
            var title = "  PIXIE hollow BaKe oFF  ";
            var result = MovieRepository.GetMovie(title);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title.ToUpper(), Is.EqualTo(title.Trim().ToUpper())); //case insensitive match, trimmed
        }

        [Test]
        public void GetMoviesByGenreReturnsMovies()
        {
            DataSource.Genres.ForEach(g => Console.WriteLine($"genre:{g}"));
            var genre = "TV Movie";
            var results = MovieRepository.GetMoviesByGenre(genre);
            Assert.That(results.Count, Is.EqualTo(214));
        }

        [Test]
        public void GetMoviesByGenreReturnsMoviesWithCaseInsenstiveSearch()
        {
            var genre = "Tv MoViE";
            var results = MovieRepository.GetMoviesByGenre(genre);
            Assert.That(results.Count, Is.EqualTo(214));
        }

        [Test]
        public void GetMoviesByGenreReturnsMoviesWithTrimmedSearch()
        {
            var genre = "  TV Movie  ";
            var results = MovieRepository.GetMoviesByGenre(genre);
            Assert.That(results.Count, Is.EqualTo(214));
        }

        [Test]
        public void GetGenresReturnsAllGenres()
        {
            var results = MovieRepository.GetGenres()
                .ToList();

            Assert.That(results.Count == DataSource.Genres.Count);
        }

        [Test]
        public void GetMoviesReturnsAllMovies()
        {
            var results = MovieRepository.GetMovies()
                .ToList();

            Assert.That(results.Count, Is.EqualTo(DataSource.Movies.Count));
        }
    }
}
