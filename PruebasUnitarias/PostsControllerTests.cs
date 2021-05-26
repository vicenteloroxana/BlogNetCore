using Blog.Controllers;
using Blog.Models;
using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Tests.PruebasUnitarias
{
    [TestClass]
    public class PostsControllerTests: BasePruebas
    {
        [TestMethod]
        public void TestViewCreate(){
            PostsController controller = new PostsController();

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethodDetails(){
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.Posts.Add(new Post(){
                Titulo = "Informatica sigo XX",
                Contenido = "Informatica",
                Categoria = "Tecnologia",
                Fecha_de_creacion = new DateTime(),
                Imagen = new byte[1]
            });
            contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            var controller = new PostsController(contexto2);
            var respuesta = controller.Details(1) as ViewResult;
            var post = (PostDTO) respuesta.ViewData.Model;

            Assert.AreEqual("Informatica sigo XX", post.Titulo);
        }
    }
}
