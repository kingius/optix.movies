using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Interfaces
{
    /// <summary>
    /// A movie genre.
    /// </summary>
    public interface IGenre
    {
        /// <summary>
        /// The database id for this movie.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The name of a genre.
        /// </summary>
        [MaxLength(128)]
        string Name { get; set; }
    }
}
