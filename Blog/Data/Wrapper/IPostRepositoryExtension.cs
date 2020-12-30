using Blog.Models;
using Blog.Models.Comments;

namespace Blog.Data.Wrapper
{
    /// <summary>
    /// Repository extension implementing specific functions related to Post entity.
    /// </summary>
    public interface IPostRepositoryExtension
    {
        /// <summary>
        /// Returns specific post entity with included nested properties.
        /// </summary>
        /// <param name="id">Post identifier.</param>
        /// <returns></returns>
        Post GetIncludedPostEntities(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        void AddSubComment(SubComment comment);
    }
}
