namespace SocialApp.Dtos
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
