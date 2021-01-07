using Blog.Domain.Entities.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.DTO
{
    public class PostDto
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

        /// <summary>
        /// Reference to the main comments belonged to this post.
        /// </summary>
        public List<MainComment> MainComments { get; set; }
    }
}
