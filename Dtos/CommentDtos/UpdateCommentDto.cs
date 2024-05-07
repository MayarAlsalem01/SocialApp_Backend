namespace SocialApp.Dtos.CommentDtos
{
    public class UpdateCommentDto:BaseCommentDto
    {
        public Guid CommentId { get; set; }
        public UpdateCommentDto()
        {
                
        }
    }
}
