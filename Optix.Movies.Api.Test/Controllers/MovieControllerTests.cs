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
    class MovieControllerTests
    {
        /// <summary>
        /// The controller under test.
        /// </summary>
        private MovieController Controller { get; set; }

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
            var logger = new Mock<ILogger<MovieController>>();

            Controller = new MovieController(movieRepository.Object, logger.Object);
        }


        [Test]
        public void GetReturnsMatchingMovie()
        {
            var match = Controller.Get("two");
            Assert.That(match == Movies[1]);
        }
    }
}
