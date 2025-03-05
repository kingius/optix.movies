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
    class MoviesControllerTests
    {
        /// <summary>
        /// The controller under test.
        /// </summary>
        private MoviesController Controller { get; set; }

        /// <summary>
        /// The genres to test for.
        /// </summary>
        private List<Movie> Movies { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Movies = new List<Movie>()
            {
                new Movie() { Title = "one"},
                new Movie() { Title = "two"},
                new Movie() { Title = "three"},
            };

            var dataSource = new Mock<IMovieDataSource>();
            dataSource.Setup(ds => ds.Movies)
                .Returns(Movies);

            var movieRepository = new Mock<MovieFileRepository>(dataSource.Object);
            var logger = new Mock<ILogger<MoviesController>>();

            Controller = new MoviesController(movieRepository.Object, logger.Object);
        }


        [Test]
        public void GetReturnsAllMovies()
        {
            var results = Controller.Get(1, 1000)
                .Items;

            Assert.That(results.Count, Is.EqualTo( Movies.Count));
            for (int i = 0; i < Movies.Count; i++) //new list, thanks to skip and take, but the contents should exactly match.
            {
                Assert.That(results[i], Is.EqualTo(Movies[i]));
            }
        }
    }
}
