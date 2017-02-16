using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.NewDb.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public List<Post> Posts { get; set; }
        public string Url { get; set; }
    }

    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }

    public class Post
    {
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
    }
}