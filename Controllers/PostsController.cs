using Blog.Data;
using Blog.Models;
using Blog.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }
        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var postsBD = await(from post in _context.Posts
                                orderby post.Fecha_de_creacion descending
                                select new
                                {
                                    ID = post.ID,
                                    Titulo = post.Titulo,
                                    Imagen = post.Imagen,
                                    Categoria = post.Categoria,
                                    Fecha = post.Fecha_de_creacion
                                }).ToListAsync();

            List<PostDTO> postsDTO = postsBD
                .Select(post => new PostDTO()
                {
                    ID = post.ID,
                    Titulo = post.Titulo,
                    Imagen = post.Imagen,
                    Categoria = post.Categoria,
                    Fecha_de_creacion = post.Fecha
                }).ToList();

            return View(postsDTO);
        }

        // GET: PostsController/Details/5
        public IActionResult Details(int idPost) {
            Post postDB = _context.Posts.Find(idPost);
            bool validateIfDataIsNull = postDB == null;

            if (validateIfDataIsNull) {
                return NotFound();
            }

            PostDTO postDTO = new PostDTO()
            {
                ID = postDB.ID,
                Titulo = postDB.Titulo,
                Imagen = postDB.Imagen,
                Contenido = postDB.Contenido,
                Categoria = postDB.Categoria,
                Fecha_de_creacion = postDB.Fecha_de_creacion
            };

            return View(postDTO);
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
