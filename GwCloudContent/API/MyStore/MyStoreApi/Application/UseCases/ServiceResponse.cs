using System.Net;

namespace MyStoreApi.UseCases
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public HttpStatusCode HttpCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public void Response(HttpStatusCode httpCode, bool success = true)
        {
            Success = success;
            HttpCode = httpCode;
        }
        
        public void Response(string message, bool success = true)
        {
            Success = success;
            Message = message;
        }

        public void Response(HttpStatusCode httpCode, 
            string message = "", 
            bool success = true)
        {
            HttpCode = httpCode;
            Success = success;
            Message = message;
        }

        public void Response(T data, 
            HttpStatusCode httpCode, 
            string message = "", 
            bool success = true)
        {
            Data = data;
            HttpCode = httpCode;
            Success = success;
            Message = message;
        }
    }
}