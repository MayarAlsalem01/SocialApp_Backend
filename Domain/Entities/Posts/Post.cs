using SocialApp.Domain.Entities.Comments;

namespace SocialApp.Domain.Entities.Posts
{
    public class Post:BaseEntity
    {

       
        

        public string Content { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public string? PostImage { get; set; }
        public ICollection<Comment> Comments{ get; set; }
       
    }
}
