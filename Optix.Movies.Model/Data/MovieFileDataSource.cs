using CsvHelper;
using CsvHelper.Configuration;
using Optix.Movies.Model.Interfaces;
using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Data
{
    /// <inheritdoc/>
    public class MovieFileDataSource: IMovieDataSource
    {
        /// <inheritdoc/>
        public List<Movie> Movies { get; private set; }

        /// <inheritdoc/>
        public List<Genre> Genres { get; private set; }

        public MovieFileDataSource()
        {
            Movies = new List<Movie>();

            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = exePath.Substring(0,exePath.LastIndexOf('\\'));
            Connect(path + @"\data\mymoviedb.csv");
        }

        /// <summary>
        /// Connect to the data set so it is ready.
        /// </summary>
        /// <param name="csvPath">Path to the CSV file.</param>
        private void Connect(string csvPath)
        {
            if (csvPath is null) throw new ArgumentNullException(nameof(csvPath));

            AddMovieDataSet(csvPath);
            AddGenreDataSet();
        }

        /// <summary>
        /// Add the movie data set.
        /// </summary>
        /// <param name="filePath">Path to the CSV file.</param>
        private void AddMovieDataSet(string filePath)
        {
            if (filePath is null) throw new ArgumentNullException(nameof(filePath));

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = "\n",
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<MovieObjectMap>();
                Movies = csv.GetRecords<Movie>().ToList();
            }
        }

        /// <summary>
        /// Add the genre data set.
        /// </summary>
        private void AddGenreDataSet()
        {
            var uniqueGenres = new Dictionary<string, Genre>();
            Movies.ForEach(m =>
            {
                m.AddGenres(m.Genre.Split(","));

                for (int i = 0; i < m.Genres.Count; i++)
                {
                    var lowered = m.Genres[i].Name.ToLower().Trim(); //optimise for searches later.
                    m.Genres[i].Name = lowered;

                    if (!uniqueGenres.ContainsKey(lowered))
                    {
                        uniqueGenres.Add(lowered, new Genre() { Name = lowered });
                    }
                }
            });

            Genres = uniqueGenres.Values.ToList();
        }
    }
}
