using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Post> _repository;

        private readonly IFileManager _fileManager;

        public HomeController(IRepository<Post> repository, IFileManager fileManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public IActionResult Index(string category)
        {
            var posts = string.IsNullOrEmpty(category) 
                ? _repository.GetAll() 
                : _repository.GetAllByCondition(post => post.Category.ToLower().Equals(category.ToLower()));

            //var posts = _repository.GetAll();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repository.Get(id);

            return View(post);
        }

        /// <summary>
        /// Dynamic image streaming.
        /// </summary>
        /// <param name="image">Image title.</param>
        /// <returns>Stream of the requested image.</returns>
        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(".") + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}
