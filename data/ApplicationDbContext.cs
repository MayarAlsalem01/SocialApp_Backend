using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialApp.data.Configuration;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Entities.Comments;
using SocialApp.Domain.Entities.Posts;

namespace SocialApp.data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Comment> Comments{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role { Name="admin"}, new Role { Name = "user" });
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
          
        }
    }
    
}
