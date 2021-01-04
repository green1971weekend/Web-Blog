using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
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
        Post GetPostIncludedEntities(int id);

        /// <summary>
        /// Add the passing sub comment to the database.
        /// </summary>
        /// <param name="comment">Sub comment.</param>
        void AddSubComment(SubComment comment);

        /// <summary>
        /// Compute the capacity of all posts and return limited amount of posts.
        /// </summary>
        /// <param name="pageNumber">Current page a user on.</param>
        /// <param name="category"></param>
        /// <returns></returns>
        IndexViewModel GetAllPostsByPagination(int pageNumber, string category);
    }
}
