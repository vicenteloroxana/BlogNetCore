using Blog.Controllers;
using Blog.Models;
using Blog.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
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
            // preparacion-----> instanciacion del controlador
            PostsController controller = new PostsController();

            // prueba-----> invocacion del metodo Create
            ViewResult result = controller.Create() as ViewResult;

            // verificacion----> verificaion del resultado esperado
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethodDetails(){
            // preparacion---> crer BD, Post y guardarlo
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

            // prueba----> invocar al metodo del controlador
            var respuesta = controller.Details(1) as ViewResult;
            var post = (PostDTO) respuesta.ViewData.Model;

            // verificacion---> comprueba que el post contenido en los datos de la vista tiene el nombre "Informatica siglo XX"
            Assert.AreEqual("Informatica sigo XX", post.Titulo);
        }

        [TestMethod]
        public void TestMethodCreate()
        {
            // preparacion---> crear BD, contexto, Post y instanciacion de FormFile y del controlador
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var contexto2 = ConstruirContext(nombreBD);

            var nuevoPost = new PostDTO()
            {
                Titulo = "Covid",
                Contenido = "Origen de la pandemia",
                Categoria = "Salud",
                Fecha_de_creacion = new DateTime()
            };

            var stream = File.OpenRead(@"./Images/dart.jpg");
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./Images/dart.jpg"))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var controller = new PostsController(contexto);

            // prueba--> invocar el metodo Create junto al modelo DTO  y el FormFile
            var respuesta = controller.Create(nuevoPost, file);

            // verificacion ---> usar el otro contexto para verificar que en la BD hay un elemento
            var cantidad = contexto2.Posts.Count();
            Assert.AreEqual(1, cantidad);
        }
    }
}
