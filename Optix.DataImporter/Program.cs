using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Optix.Movies.Model.Data;
using Optix.Movies.Model.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Optix.DataImporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            var connectionString = config.GetSection("ConnectionStrings")
                .GetSection("DefaultConnection")
                .Value;
            Console.WriteLine($"connectionString={connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var db = new MovieDbContext(optionsBuilder.Options);
            var genreCount = db.Database.SqlQuery<int>(FormattableStringFactory.Create("SELECT COUNT(ID) FROM genres")).ToList().First();
            Console.WriteLine($"Genres={genreCount}");

            var movieCount = db.Database.SqlQuery<int>(FormattableStringFactory.Create("SELECT COUNT(ID) FROM movies")).ToList().First();
            Console.WriteLine($"Movies={movieCount}");

            if (genreCount > 0 || movieCount > 0)
            {
                var deleteCommand = Ask("Do you wish to wipe out any existing data in the database tables?");
                if (deleteCommand)
                {
                    DeleteMovies(db);
                    DeleteGenres(db);
                }
            }

            ImportData(db);
            Console.WriteLine($"All done!");

        }

        static bool Ask(string question)
        {
            ConsoleKey response;
            do
            {
                Console.Write($"{question} [y/n] ");
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return response == ConsoleKey.Y;
        }

        static void DeleteMovies(MovieDbContext db)
        {
            Console.WriteLine("Deleting movies");
            _ = db.Database.ExecuteSql(FormattableStringFactory.Create("DELETE FROM GenreMovie"));
            _ = db.Database.ExecuteSql(FormattableStringFactory.Create("DELETE FROM Movies"));

        }
        static void DeleteGenres(MovieDbContext db)
        {
            Console.WriteLine("Deleting genres");
            _ = db.Database.ExecuteSql(FormattableStringFactory.Create("DELETE FROM Genres"));
        }

        static void ImportData(MovieDbContext db)
        {
            Console.WriteLine("Importing data");
            var importData = new MovieFileDataSource();
            importData.Genres.ForEach(g => {
                Console.Write("\x000DImporting genre: " + g.Name + "                          ");
                db.Genres.Add(new Genre() { Name = g.Name });
            });

            db.SaveChanges();

            Console.WriteLine($"");

            var dbGenreCache = new Dictionary<string, Genre>();
            importData.Movies.ToList().ForEach(m =>
            {
                Console.Write("\x000DImporting movie: " + m.Title + "                                           ");
                var unattachedGenres = m.Genres.Select(g => g.Name).ToList();
                m.Genres = new List<Genre>();
                
                unattachedGenres.ForEach(ug =>
                {
                    if (!dbGenreCache.ContainsKey(ug))
                    {
                        dbGenreCache[ug] = db.Genres.First(g => g.Name == ug);
                    }

                    m.Genres.Add(dbGenreCache[ug]);
                });

                db.Movies.Add((Movie)m);
            });

            Console.WriteLine($"");

            db.SaveChanges();
        }
    }
}
