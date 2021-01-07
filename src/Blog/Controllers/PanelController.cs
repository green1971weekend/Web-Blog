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
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository<Post> _repository;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository<Post> repository, IFileManager fileManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAll();

            var dtoPosts = new List<PostDto>();

            foreach(var post in posts)
            {
                var postDto = new PostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    Image = post.Image,
                    Description = post.Description,
                    Tags = post.Tags,
                    Category = post.Category,
                    Created = post.Created,
                    MainComments = post.MainComments
                };

                dtoPosts.Add(postDto);
            }

            return View(dtoPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.Remove(_repository.Get(id));
            await _repository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }

            var post = _repository.Get((int)id);
            return View(new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CurrentImage = post.Image,
                Description = post.Description,
                Category = post.Category,
                Tags = post.Tags
            });
        }

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
