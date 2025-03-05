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
    public class Movie : IMovie
    {
        /// <inheritdoc/>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        /// <inheritdoc/>
        public DateTime ReleaseDate { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string Overview { get; set; }

        /// <inheritdoc/>
        public float Popularity { get; set; }

        /// <inheritdoc/>
        public int VoteCount { get; set; }

        /// <inheritdoc/>
        public float VoteAverage { get; set; }

        /// <inheritdoc/>
        public string OriginalLanguage { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string Genre { get; set; }

        /// <inheritdoc/>
        public List<Genre> Genres { get; set; }

        /// <inheritdoc/>
        public string PosterUrl { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Movie()
        {
            Genres = new List<Genre>();
        }

        /// <summary>
        /// Helper method for adding genres to a movie.
        /// </summary>
        /// <param name="genres">List of genres.</param>
        public void AddGenres(string[] genres)
        {
            foreach (var genreName in genres)
            {
                Genres.Add(new Genre() { Name = genreName });
            }
        }
    }
}
