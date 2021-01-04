using Blog.Data.Repository;
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

        private readonly IRepository<Post> _repository;

        public PostRepositoryExtension(IRepository<Post> repository, AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
                Pages = PageNumbers(pageNumber, pageCount),
                Category = category,
                Posts = query.Skip(skipAmount)
                             .Take(pageSize)
                             .ToList()
            };
        }

        /// <summary>
        /// Page list rendering.
        /// </summary>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageCount">Computed common amount of pages.</param>
        /// <returns></returns>
        private IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
        {
            List<int> pages = new List<int>();

            int midPoint = pageNumber < 3 ? 3
                : pageNumber > pageCount - 2 ? pageCount - 2
                : pageNumber;

            for (int i = midPoint - 2; i <= midPoint + 2; i++)
            {
                pages.Add(i);
            }

            //Smart pagination based on three dots addition to begin and end of pagination list.
            if (pages[0] != 1)
            {
                pages.Insert(0, 1);

                if (pages[1] - pages[0] > 1)
                {
                    pages.Insert(1, -1);
                }
            }

            if (pages[pages.Count - 1] != pageCount)
            {
                pages.Insert(pages.Count, pageCount);
                if (pages[pages.Count - 1] - pages[pages.Count - 2] > 1)
                {
                    pages.Insert(pages.Count - 1, -1);
                }
            }

            return pages;
        }
    }
}
