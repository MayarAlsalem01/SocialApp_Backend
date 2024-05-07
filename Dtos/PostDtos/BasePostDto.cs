using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.PostDtos
{
    public class BasePostDto
    {
        
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
    }
}
