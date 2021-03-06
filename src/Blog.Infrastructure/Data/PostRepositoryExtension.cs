﻿using Blog.Application.DTO;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Entities.Comments;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blog.Infrastructure.Data
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
        public PostCollectionDto GetAllPostsByPagination(int pageNumber, string category, string search)
        {
            var pageSize = 5;
            var skipAmount = pageSize * (pageNumber - 1);
            var capacity = skipAmount + pageSize;

            // If there is no need to update loaded entities use AsNoTracking method which helps with performance optimization.
            var query = _context.Posts.AsNoTracking()
                                        .AsQueryable()
                                        .OrderBy(post => post.Id);

            if (!string.IsNullOrEmpty(category))
            {
                query = (IOrderedQueryable<Post>)query
                            .Where(p => p.Category.ToLower().Equals(category.ToLower()));
            }

            if (!string.IsNullOrEmpty(search))
                // EF.Functions.Like - is basically Contains function but more performant sql version for EntityFramework.
                query = (IOrderedQueryable<Post>)query
                            .Where(x => EF.Functions.Like(x.Title, $"%{search}%")
                                    || EF.Functions.Like(x.Body, $"%{search}%")
                                    || EF.Functions.Like(x.Description, $"%{0}%"));

            var postCount = query.Count();
            var pageCount = (int)Math.Ceiling((double)postCount / pageSize);

            return new PostCollectionDto
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                NextPage = postCount > capacity,
                Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
                Category = category,
                Search = search,
                Posts = query.Skip(skipAmount)
                             .Take(pageSize)
                             .ToList()
            };
        }
    }
}
