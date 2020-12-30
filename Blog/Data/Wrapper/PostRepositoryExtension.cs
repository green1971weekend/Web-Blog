using Blog.Models;
using Blog.Models.Comments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Data.Wrapper
{
    ///<inheritdoc cref="IPostRepositoryExtension"/>
    public class PostRepositoryExtension : IPostRepositoryExtension
    {

        private readonly AppDbContext _context;

        public PostRepositoryExtension(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        ///<inheritdoc/>
        public Post GetIncludedPostEntities(int id)
        {
            return _context.Posts
                .Include(p => p.MainComments)
                    //If to include again after first include we're gonna be again in post property which means we won't be able to reach and retreive SubComments.
                    .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        ///<inheritdoc/>
        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }

        ///<inheritdoc/>
        public List<Post> GetAllPostsByPagination(int pageNumber)
        {
            int pageSize = 5;
            int pageCount = _context.Posts.Count() / pageSize;
            return _context.Posts
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
        }
    }
}
