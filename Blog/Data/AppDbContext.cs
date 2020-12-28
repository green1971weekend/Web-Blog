using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    /// <summary>
    /// Context for EF Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            :base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}
