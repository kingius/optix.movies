using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Optix.Movies.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Optix.Movies.Model.Data
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        /// <summary>
        /// The movies.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// The genres.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Fluent specifications.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Ignore(m => m.Genre);
        }

        /// <summary>
        /// Simple logging.
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.LogTo(message => Debug.WriteLine(message), new[] { RelationalEventId.CommandExecuting } );
    }
}
