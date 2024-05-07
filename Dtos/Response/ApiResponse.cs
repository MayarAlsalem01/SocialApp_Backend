namespace SocialApp.Dtos.Response
{
    public class ApiResponse<T>
    {
        public int statusCode { get; set; } = 200;
        public string message { get; set; }
        public T Response { get; set; }
        public ApiResponse(int statusCode, string message, T response)
        {
            this.statusCode = statusCode;
            this.message = message;
            Response = response;
        }
        public ApiResponse()
        {

        }
       
        
    }
}
