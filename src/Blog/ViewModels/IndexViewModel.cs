using Blog.Domain.Entities;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    /// <summary>
    /// View model captures on data from front-end razor pages.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Post list retrieved by certain conditions.
        /// </summary>
        public IEnumerable<Post> Posts { get; set; }

        /// <summary>
        /// List of current accessible pages in a certain range.
        /// </summary>
        public IEnumerable<int> Pages { get; set; }

        /// <summary>
        /// Common amount of pages for all posts.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Current page number the user on.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Indicates to possibility switch to next page of posts.
        /// </summary>
        public bool NextPage { get; set; }

        /// <summary>
        /// Category of posts for filtering.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Search input string from user.
        /// </summary>
        public string Search { get; set; }
    }
}
