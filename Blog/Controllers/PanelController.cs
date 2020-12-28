using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository<Post> _repository;

        public PanelController(IRepository<Post> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAll();
            return View(posts);
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
                return View(new Post());
            }

            var post = _repository.Get((int)id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
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
