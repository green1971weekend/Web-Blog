using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    /// <summary>
    /// Context for EF Core. Inherited from IdentityDbContext which has the db entities as IdentityUser, IdentityRole etc.
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            :base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}
