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
    class GenresControllerTests
    {
        /// <summary>
        /// The controller under test.
        /// </summary>
        private GenresController Controller { get; set; }

        /// <summary>
        /// The genres to test for.
        /// </summary>
        private List<Genre> Genres { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Genres = new List<Genre>()
            {
                new Genre() { Name = "one" },
                new Genre() { Name = "two" },
                new Genre() { Name = "three" },
            };

            var dataSource = new Mock<IMovieDataSource>();
            dataSource.Setup(ds => ds.Genres)
                .Returns(Genres);

            var movieRepository = new Mock<MovieFileRepository>(dataSource.Object);
            var logger = new Mock<ILogger<GenresController>>();

            Controller = new GenresController(movieRepository.Object, logger.Object);
        }


        [Test]
        public void GetReturnsAllGenres()
        {
            var results = Controller.Get();

            Assert.That(results.Count == Genres.Count);
            for (int i = 0; i < results.Count; i++) //contents should exactly match.
            {
                Assert.That(results[i], Is.EqualTo(Genres[i]));
            }
        }
    }
}
