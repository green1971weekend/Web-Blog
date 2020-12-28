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
            var posts = _repository.GetAll();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repository.Get(id);

            return View(post);
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
            if(id == null)
            {
                return View(new Post());
            }

            var post = _repository.Get((int) id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if(post.Id > 0)
                _repository.Update(post);
            else
                _repository.Add(post);


            if(await _repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }
    }
}
