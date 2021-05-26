using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests
{
    public class BasePruebas
    {
        protected BlogContext ConstruirContext(string nombreDB) {
            var opciones = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(nombreDB).Options;
            var dbContext = new BlogContext(opciones);

            return dbContext;
        }
    }
}
