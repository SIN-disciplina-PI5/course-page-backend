using Newtonsoft.Json;
using System.Net;

namespace UNICAP.SiteCurso.Application.DTOs.GenericsFolder
{
    public class Response
    {
        [JsonProperty("errorMessages")]
        public IList<string> ErrorMessages { get; protected set; } = new List<string>();
        [JsonProperty("hasErrors")]
        public bool HasErrors { get; protected set; }
        [JsonProperty("message")]
        public string Message { get; protected set; }
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; protected set; }
        [JsonProperty("collections")]
        public object collections { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }


        public void AddErrorMessages(string message)
        {
            ErrorMessages.Add(message);
            HasErrors = true;
        }

        public void SetStatusCode(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        internal Task<Response> GenerateResponse(bool hasError, HttpStatusCode statusCode, object message)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GenerateResponse(HttpStatusCode statusCode = HttpStatusCode.OK, bool hasError = default, string message = default, object collection = default, int count = default)
        {
            StatusCode = statusCode;
            HasErrors = hasError;
            Message = message;
            collections = collection;
            Count = count;
            return Task.FromResult(this);
        }
    }
}
