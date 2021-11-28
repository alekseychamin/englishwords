using System.Net;

namespace PublicApi.Endpoints.Errors
{
    public class ErrorResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}