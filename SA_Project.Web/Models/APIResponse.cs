using System.Net;

namespace SA_Project.Web.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object? Result { get; set; }
    }
}
