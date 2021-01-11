using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    /// <summary>
    /// Controller for managing the application by admin.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository<Post> _repository;

        private readonly IFileManager _fileManager;

        private readonly IMapper _mapper;

        public PanelController(IRepository<Post> repository, 
                                IFileManager fileManager,
                                IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Render all existing posts.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var posts = _repository.GetAll();

            var dtoPosts = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts);

            return View(dtoPosts);
        }

        /// <summary>
        /// Remove certain post by identifier.
        /// </summary>
        /// <param name="id">Post identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.Remove(_repository.Get(id));
            await _repository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Render certain post for edition by identifier or create a new one.
        /// </summary>
        /// <param name="id">Post identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PostViewModel());

            var post = _repository.Get((int)id);
            var vmPost = _mapper.Map<Post, PostViewModel>(post);

            return View(vmPost);
        }

        /// <summary>
        /// Edit certain post by identifier.
        /// </summary>
        /// <param name="vm">Post view model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Image = _fileManager.SaveImage(vm.Image),
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
            {
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                {
                    _fileManager.RemoveImage(vm.CurrentImage);
                }
                post.Image = _fileManager.SaveImage(vm.Image);
            }

            if (post.Id > 0)
                _repository.Update(post);
            else
                _repository.Add(post);


            if (await _repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }
    }
}
