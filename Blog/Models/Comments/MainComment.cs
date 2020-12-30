using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.Comments
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
