using Blog.Models;
using Blog.Models.Comments;
using System.Collections.Generic;

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
        /// Add the passing sub comment to the database.
        /// </summary>
        /// <param name="comment">Sub comment.</param>
        void AddSubComment(SubComment comment);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        List<Post> GetAllPostsByPagination(int pageNumber);
    }
}
