using Microsoft.AspNetCore.Http;
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
    public class PostViewModel
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
        /// Default image.
        /// </summary>
        public string CurrentImage { get; set; }

        /// <summary>
        /// Title of the image. IFormFile is basically an interface for any sort of file, image, video etc.
        /// </summary>
        public IFormFile Image { get; set; } = null;

        /// <summary>
        /// Date time creation of post.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
