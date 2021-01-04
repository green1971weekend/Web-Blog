using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    /// <summary>
    /// View model captures on data from front-end razor pages.
    /// </summary>
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<int> Pages { get; internal set; }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public bool NextPage { get; set; }

        public string Category { get; set; }
    }
}
