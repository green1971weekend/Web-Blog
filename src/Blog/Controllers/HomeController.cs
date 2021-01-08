using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Entities.Comments;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Post> _repository;

        private readonly IFileManager _fileManager;

        private readonly IPostRepositoryExtension _postRepositoryExtension;

        private readonly IMapper _mapper;

        public HomeController(IRepository<Post> repository,
                                IFileManager fileManager,
                                IPostRepositoryExtension postRepositoryExtension,
                                IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            _postRepositoryExtension = postRepositoryExtension ?? throw new ArgumentNullException(nameof(postRepositoryExtension));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Render specific amount of posts given the arguments as current page number, category, user search input.
        /// </summary>
        /// <param name="pageNumber">Current page number the user on.</param>
        /// <param name="category">Category of posts.</param>
        /// <param name="search">User search input.</param>
        /// <returns></returns>
        public IActionResult Index(int pageNumber, string category, string search)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });

            var postCollection = _postRepositoryExtension.GetAllPostsByPagination(pageNumber, category, search);
            var vm = _mapper.Map<PostCollectionDto, PostCollectionViewModel>(postCollection);

            return View(vm);
        }

        /// <summary>
        /// Render the all categories view (category pictures defined in static way.)
        /// </summary>
        /// <returns></returns>
        public IActionResult Category() => View();

        /// <summary>
        /// Render a certain post and all information related to it(included comments).
        /// </summary>
        /// <param name="id">Post identifier.</param>
        /// <returns></returns>
        public IActionResult Post(int id)
        {
            var post = _postRepositoryExtension.GetPostIncludedEntities(id);
            var postDto = _mapper.Map<Post, PostDto>(post);

            return View(postDto);
        }

        /// <summary>
        /// Dynamic image streaming.
        /// </summary>
        /// <param name="image">Image title.</param>
        /// <attributes>Response cache allows to cache resulted images. CacheProfileName indicates to profile defined in startup.cs</attributes>
        /// <returns>Stream of the requested image.</returns>
        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(".") + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }

        /// <summary>
        /// Creates a new main or sub comment under the certain post by user.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = _postRepositoryExtension.GetPostIncludedEntities(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments ??= new List<MainComment>();

                post.MainComments.Add(new MainComment()
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                });

                _repository.Update(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };

                _postRepositoryExtension.AddSubComment(comment);
            }

            await _repository.SaveChangesAsync();

            return RedirectToAction("Post", new { id = vm.PostId });
        }
    }
}
