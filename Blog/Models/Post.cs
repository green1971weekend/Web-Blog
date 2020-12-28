using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    /// <summary>
    /// Post model.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Title of the post. Default value set to "" to avoid null exceptions.
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Body of the post. Default value set to "" to avoid null exceptions.
        /// </summary>
        public string Body { get; set; } = "";

        /// <summary>
        /// Date time creation of post.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
