using System.Collections.Generic;

namespace Blog.Domain.Entities.Comments
{
    /// <summary>
    /// Main comment entity belonged to specific post.
    /// </summary>
    public class MainComment : Comment
    {
        /// <summary>
        /// List of sub comments belonged to main comment.
        /// </summary>
        public List<SubComment> SubComments { get; set; }
    }
}
