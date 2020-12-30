using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public int PageNumber { get; set; }
    }
}
