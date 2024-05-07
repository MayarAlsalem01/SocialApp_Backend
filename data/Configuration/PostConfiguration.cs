using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialApp.Domain.Entities.Posts;

namespace SocialApp.data.Configuration
{
    public class PostConfiguration: IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(255);
           

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

          
                
        }
    }
}
