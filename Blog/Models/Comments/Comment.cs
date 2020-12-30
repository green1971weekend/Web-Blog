using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.Comments
{
    /// <summary>
    /// Abstract comment type which defines basic properties of entity.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Comment identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Text message of comment.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Creation date time.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
