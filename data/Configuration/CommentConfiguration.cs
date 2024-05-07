using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialApp.Domain.Entities.Comments;

namespace SocialApp.data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c=>c.User)
                .WithMany(u=>u.Comments)
                .HasForeignKey(u=>u.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
