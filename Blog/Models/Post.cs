using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    /// <summary>
    /// Post model for read/write action to database.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Post identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the post. Default value set to "" to avoid null exceptions.
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Body of the post. Default value set to "" to avoid null exceptions.
        /// </summary>
        public string Body { get; set; } = "";

        /// <summary>
        /// Title of the image.
        /// </summary>
        public string Image { get; set; } = "";

        /// <summary>
        /// Description of post using by search engine optimization.
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Tags of post using by search engine optimization.
        /// </summary>
        public string Tags { get; set; } = "";

        /// <summary>
        /// Category of post using by search engine optimization.
        /// </summary>
        public string Category { get; set; } = "";

        /// <summary>
        /// Date time creation of post.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
