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

        public HomeController(IRepository<Post> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAll();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repository.Get(id);

            return View(post);
        }
    }
}
