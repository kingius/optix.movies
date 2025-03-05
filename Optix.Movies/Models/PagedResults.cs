using System.Collections.Generic;

namespace Optix.Movies.Api.Models
{
    /// <summary>
    /// Results class for paging.
    /// </summary>
    /// <typeparam name="T">Model to wrap.</typeparam>
    public class PagedResults<T>
    {
        /// <summary>
        /// The total number of pages in the results.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The current page of items being viewed.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The list of <typeparamref name="T"/>.
        /// </summary>
        public List<T> Items { get; set; }
    }
}
