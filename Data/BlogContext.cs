using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            // Borrado Suave 
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Metadata.GetProperties()
                .Any(x => x.Name == "EstaBorrado"))
                .ToList();

            foreach (var entity in entities)
            {
                entity.State = EntityState.Unchanged;
                entity.CurrentValues["EstaBorrado"] = true;
            }

            return base.SaveChanges();
        }

        public DbSet<Post> Posts { get; set; }
    }
}
