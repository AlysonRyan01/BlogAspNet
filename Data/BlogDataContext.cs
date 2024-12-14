using BlogAspNet.Data.Mappings;
using BlogAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAspNet.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }

        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
        {
            
        }
        
    }
}