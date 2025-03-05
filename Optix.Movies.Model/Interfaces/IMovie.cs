using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Interfaces
{
    /// <summary>
    /// A movie in the data set.
    /// </summary>
    public interface IMovie
    {
        /// <summary>
        /// The database id for this movie.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date of release.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The movie's title.
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// Description of the movie.
        /// </summary>
        [MaxLength(1024)]
        public string Overview { get; set; }

        /// <summary>
        /// A measure of how successful the movie was.
        /// </summary>
        public float Popularity { get; set; }

        /// <summary>
        /// Number of votes.
        /// </summary>
        public int VoteCount { get; set; }

        /// <summary>
        /// Average vote.
        /// </summary>
        public float VoteAverage { get; set; }

        /// <summary>
        /// Language of the movie before subtitles/dubbing.
        /// </summary>
        [MaxLength(2)]
        public string OriginalLanguage { get; set; }

        /// <summary>
        /// A comma seperated list of genres.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public string Genre { get; set; }

        /// <summary>
        /// List of genres the movie belongs to.
        /// </summary>
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// Caption image.
        /// </summary>
        [MaxLength(256)]
        public string PosterUrl { get; set; }

        /// <summary>
        /// Helper method for adding genres to a movie.
        /// </summary>
        /// <param name="genres">List of genres.</param>
        void AddGenres(string[] genres);
    }
}
