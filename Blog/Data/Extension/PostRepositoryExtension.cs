using Blog.Data.Repository;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blog.Data.Wrapper
{
    ///<inheritdoc cref="IPostRepositoryExtension"/>
    public class PostRepositoryExtension : IPostRepositoryExtension
    {

        private readonly AppDbContext _context;

        private readonly IRepository<Post> _repository;

        public PostRepositoryExtension(IRepository<Post> repository, AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
        public IndexViewModel GetAllPostsByPagination(int pageNumber, string category)
        {
            var pageSize = 5;
            var skipAmount = pageSize * (pageNumber - 1);
            var capacity = skipAmount + pageSize;

            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.ToLower().Equals(category.ToLower()));
            }

            var postCount = query.Count();

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = (int) Math.Ceiling((double) postCount / pageSize),
                NextPage = postCount > capacity,
                Category = category,
                Posts = query.Skip(skipAmount)
                             .Take(pageSize)
                             .ToList()
            };
        }
    }
}
