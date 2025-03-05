using CsvHelper.Configuration;
using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Data
{
    /// <summary>
    /// Object map for CSV to model mapping.
    /// </summary>
    public sealed class MovieObjectMap : ClassMap<Movie>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MovieObjectMap()
        {
            Map(m => m.ReleaseDate).Name("Release_Date");
            Map(m => m.Title).Name("Title");
            Map(m => m.Overview).Name("Overview");
            Map(m => m.Popularity).Name("Popularity");
            Map(m => m.VoteCount).Name("Vote_Count");
            Map(m => m.VoteAverage).Name("Vote_Average");
            Map(m => m.OriginalLanguage).Name("Original_Language");
            Map(m => m.Genre).Name("Genre");
            Map(m => m.PosterUrl).Name("Poster_Url");
        }
    }
}
