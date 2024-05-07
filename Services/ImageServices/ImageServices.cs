using SocialApp.Dtos;
using SocialApp.IServices.ImageServices;
using System.Net.Http.Headers;

namespace SocialApp.Services.ImageServices
{

    public class ImageServices : IImageServices
    {
        public async Task<bool> IsNsfwImage(byte[] image)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://nsfw-image-classification3.p.rapidapi.com/nsfwjs/check/upload"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "ae6d98e871mshc60463e6b6baa8cp1a19a1jsnb352d3637598" },
                        { "X-RapidAPI-Host", "nsfw-image-classification3.p.rapidapi.com" },
                    },
                    Content = new MultipartFormDataContent
                    {
                       new ByteArrayContent(image)
                       {
                           Headers =
                           {
                                ContentType = new MediaTypeHeaderValue("application/octet-stream"),
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "file",
                                    FileName = "image.jpg", // Provide a filename here
                                }
                           }
                       }
                    }
                };
                using (var response = await client.SendAsync(request))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }catch (Exception )
                    {
                        throw new Exception("server cant upload image please try later");
                    }
                    var responseBody = await response.Content.ReadAsStringAsync();
                   if(responseBody.ToLower()=="false") throw new Exception("there is something happend while image processing. ");    
                    // Parse the JSON response to extract probabilities
                    var probabilities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NsfwProbabilityDto>>(responseBody);

                    // Check if any probability for NSFW categories exceeds a threshold
                    const double nsfwThreshold = 0.5;
                  
                    
                        foreach (var probability in probabilities)
                        {
                            if (probability.className.ToLower() == "sexy" || probability.className.ToLower() == "porn")
                            {
                                if (probability.probability >= nsfwThreshold)
                                {
                                    return true;
                                }
                            }
                        }
                    
                  

                    // If no NSFW category probability exceeds the threshold, return false
                    return false;

                }
            }
        }

    }
}

