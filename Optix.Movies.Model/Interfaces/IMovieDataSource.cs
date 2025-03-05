using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Interfaces
{
    /// <summary>
    /// The dataset.
    /// </summary>
    public interface IMovieDataSource
    {
        /// <summary>
        /// The movies.
        /// </summary>
        public List<Movie> Movies { get; }

        /// <summary>
        /// The genres.
        /// </summary>
        public List<Genre> Genres { get; }
    }
}
