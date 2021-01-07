using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    /// <summary>
    /// View model captures on data from front-end razor pages.
    /// </summary>
    public class CommentViewModel
    {
        /// <summary>
        /// Post identifier to which this comment belongs.
        /// </summary>
        [Required]
        public int PostId { get; set; }

        /// <summary>
        /// Main comment identifier.
        /// </summary>
        [Required]
        public int MainCommentId { get; set; }

        /// <summary>
        /// Comment body.
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}
