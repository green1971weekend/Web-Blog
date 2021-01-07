namespace Blog.Domain.Entities.Comments
{
    /// <summary>
    /// Sub comment entity belonged to main comment.
    /// </summary>
    public class SubComment : Comment
    {
        /// <summary>
        /// Foreign key references to main comment id which current sub comment belongs to.
        /// </summary>
        public int MainCommentId { get; set; }
    }
}
