using Blog.Models;
using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Blog.Data.Repository;

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
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _repository.Add(post);

            if(await _repository.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }

            return View(post);
        }
    }
}
