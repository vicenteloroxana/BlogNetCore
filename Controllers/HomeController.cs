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
    public class HomeController : Controller
    {
        private BlogContext _context;

        public HomeController(BlogContext context)
        {
            _context = context;
        }

        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var postsBD = await (from post in _context.Posts
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

        public ActionResult convertImg(int idPost) {
            var img = (from post in _context.Posts
                       where post.ID == idPost
                       select post.Imagen
                       ).FirstOrDefault();

            return File(img,"Imagenes/jpg");
        }
    }
}
