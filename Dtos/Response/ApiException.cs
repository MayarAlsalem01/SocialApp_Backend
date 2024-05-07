namespace SocialApp.Dtos.Response
{
    public sealed class ApiException
    {
        public int code { get; set; }

        public ApiException(int code, string message, Dictionary<string, string> errors)
        {
            this.code = code;
            this.message = message;
            this.errors = errors;
        }
        public ApiException()
        {
            errors = new Dictionary<string, string>();
        }
        public string message { get; set; }
        public Dictionary<string, string> errors { get; set; } = new Dictionary<string, string>();
    }
}
