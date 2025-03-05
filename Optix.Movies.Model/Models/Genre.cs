using Optix.Movies.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Models
{
    /// <inheritdoc/>
    public class Genre:IGenre
    {

        /// <inheritdoc/>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        /// <inheritdoc/>
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Movies that have this genre.
        /// </summary>
        [JsonIgnore]
        public List<Movie> Movies { get; set; }

    }
}
