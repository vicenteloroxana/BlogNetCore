using Blog.Data;
using Blog.Models;
using Blog.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Web;
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

        public PostsController()
        {
        }

        // GET: PostsController/Details/5
        public IActionResult Details(int idPost) {
            Post postDB = _context.Posts.Find(idPost);
            bool validateIfDataIsNull = postDB == null;

            if (validateIfDataIsNull) {
                return NotFound(404);
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
        public ActionResult Create(PostDTO model, IFormFile imagen)
        {
            Post newPost = new Post()
            {
                Titulo = model.Titulo,
                Contenido = model.Contenido,
                Categoria = model.Categoria,
                Fecha_de_creacion = model.Fecha_de_creacion
            };

            using (var imgTemporal = new MemoryStream()) 
            {
                imagen.CopyTo(imgTemporal);
                newPost.Imagen = imgTemporal.ToArray();
            }

            _context.Posts.Add(newPost);

            try
            {
                _context.SaveChanges();
                return RedirectToAction("Index","Home");
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
        public async Task<ActionResult> Edit(int idPost, PostDTO model, IFormFile imagen)
        {
            Post postDB = await _context.Posts.FindAsync(idPost);
            bool validateIfDataIsNull = postDB == null;

            if (validateIfDataIsNull)
            {
                return NotFound();
            }

            postDB.Titulo = model.Titulo;
            postDB.Contenido = model.Contenido;
            postDB.Categoria = model.Categoria;
            postDB.Fecha_de_creacion = model.Fecha_de_creacion;

            using (var imgTemporal = new MemoryStream()) 
            {
                imagen.CopyTo(imgTemporal);
                postDB.Imagen = imgTemporal.ToArray();
            }

            _context.Posts.Update(postDB);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Petición o Solicitud Incorrecta ");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int idPost)
        {
            Post postDB = await _context.Posts.FindAsync(idPost);
            bool validateIfDataIsNull = postDB == null;

            if (validateIfDataIsNull)
            {
                return NotFound();
            }

            _context.Posts.Remove(postDB);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Petición o Solicitud Incorrecta");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
