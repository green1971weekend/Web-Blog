using Blog.Data.Repository;
using Blog.Helper;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
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
        public Post GetPostIncludedEntities(int id)
        {
            return _context.Posts
                .Include(p => p.MainComments)
                    //If to include again after first include we're gonna be again in post property which means we won't be able to reach and retrieve SubComments.
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
            var pageCount = (int)Math.Ceiling((double)postCount / pageSize);

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                NextPage = postCount > capacity,
                Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
                Category = category,
                Posts = query.Skip(skipAmount)
                             .Take(pageSize)
                             .ToList()
            };
        }
    }
}
