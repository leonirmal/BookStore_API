using Newtonsoft.Json;
using System.Net;

namespace BookStoreAPI._Core.ResponseModels
{
    public class Result<T>
    {
        public Result()
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            IsSuccess = false;
            Message = "The application has encountered an unknown error. It doesn't appear to have affected your data, but our technical staff have been automatically notified and will be looking into this with the utmost urgency.";
        }

        public Result(T? data, bool success, string message, HttpStatusCode statusCode)
        {
            Data = data;
            IsSuccess = success;
            Message = message;
            StatusCode = (int)statusCode;
        }

        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
