using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Data.Wrapper;
using Blog.Models;
using Blog.Models.Comments;
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
        public HomeController(IRepository<Post> repository, 
                                IFileManager fileManager,
                                IPostRepositoryExtension postRepositoryExtension)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            _postRepositoryExtension = postRepositoryExtension ?? throw new ArgumentNullException(nameof(postRepositoryExtension));
        }

        public IActionResult Index(int pageNumber, string category)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });

            var vm = new IndexViewModel
            {
                PageNumber = pageNumber,
                Posts = string.IsNullOrEmpty(category)
                    ? _postRepositoryExtension.GetAllPostsByPagination(pageNumber)
                    : _repository.GetAllByCondition(post => post.Category.ToLower().Equals(category.ToLower()))
            };

            return View(vm);
        }

        public IActionResult Post(int id)
        {
            var post = _postRepositoryExtension.GetIncludedPostEntities(id);

            return View(post);
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

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = _postRepositoryExtension.GetIncludedPostEntities(vm.PostId);

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

            return View();
        }
    }
}
