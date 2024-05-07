using System.ComponentModel.DataAnnotations;

namespace SocialApp.Dtos.PostDtos
{
    sealed public class CreatePostDto:BasePostDto
    {

        public IFormFile? File { set; get; }


    }
}
