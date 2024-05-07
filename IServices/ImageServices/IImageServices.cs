namespace SocialApp.IServices.ImageServices
{
    public interface IImageServices
    {
        Task<bool> IsNsfwImage(byte[]image);
    }
}
