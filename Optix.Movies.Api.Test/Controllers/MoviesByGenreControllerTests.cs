using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Optix.Movies.Api.Controllers;
using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Models;
using Optix.Movies.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Api.Test.Controllers
{
    class MoviesByGenreControllerTests
    {
        /// <summary>
        /// The controller under test.
        /// </summary>
        private MoviesByGenreController Controller { get; set; }

        /// <summary>
        /// The genres to test for.
        /// </summary>
        private List<Movie> Movies { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Movies = new List<Movie>()
            {
                new Movie() { Title = "one", Genres = new List<Genre> { new Genre() { Name = "comedy" } } },
                new Movie() { Title = "two", Genres = new List<Genre> { new Genre() { Name = "comedy" }, 
                    new Genre() { Name = "action"}} },
                new Movie() { Title = "three", Genres = new List<Genre> { new Genre() { Name = "action"} } },
            };

            var dataSource = new Mock<IMovieDataSource>();
            dataSource.Setup(ds => ds.Movies)
                .Returns(Movies);

            var movieRepository = new Mock<MovieFileRepository>(dataSource.Object);
            var logger = new Mock<ILogger<MoviesByGenreController>>();

            Controller = new MoviesByGenreController(movieRepository.Object, logger.Object);
        }


        [Test]
        public void GetReturnsMatchingMoviesByGenre()
        {
            var results = Controller.Get("action")
                .Items;

            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results[0], Is.EqualTo(Movies[1]));
            Assert.That(results[1], Is.EqualTo(Movies[2]));
        }
    }
}
