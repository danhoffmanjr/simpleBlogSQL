using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Entities;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBlogSQL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View(_postRepository.GetAll());
        }

        // GET: Post/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View(_postRepository.GetById(id));
        //}

        // GET: Post/Details/Permalink
        public IActionResult Details(string permalink)
        {
            PostViewModel viewmodel = new PostViewModel();
            viewmodel.Post = _postRepository.GetByPermalink(permalink);
            return View(viewmodel);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post newPost, IFormCollection collection)
        {
            try
            {
                _postRepository.Create(newPost);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_postRepository.GetById(id));
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post editedPost, IFormCollection collection)
        {
            try
            {
                _postRepository.Update(editedPost);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_postRepository.GetById(id));
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _postRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Rate/5
        public IActionResult Rate()
        {
            return View();
        }

        // POST: Blog/Rate/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Rate/{id?}")]
        public IActionResult Score(Rating newRating)
        {
            try
            {
                _postRepository.CreateRating(newRating);
                return View(_postRepository.GetById(newRating.Id));
            }
            catch (Exception ex)
            {
                Console.Write("Hey, You have an error exception in the Score method");
                throw ex;
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rate(Rating newRating)
        {
            try
            {
                _postRepository.CreateRating(newRating);
                //string returnPerma = _postRepo.GetById(id).Permalink;
                // return RedirectToAction(nameof(Post), new { permalink = returnPerma });
                return View();
            }
            catch (Exception ex)
            {
                Console.Write("Hey, You have an error exception in the Save Score method");
                throw ex;
            }
        }
    }
}