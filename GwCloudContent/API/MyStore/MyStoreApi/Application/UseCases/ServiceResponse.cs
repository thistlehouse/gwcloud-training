using System.Net;

namespace MyStoreApi.UseCases
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public HttpStatusCode HttpCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public ServiceResponse<T> Response(T data,
            HttpStatusCode httpCode,
            bool success = true)
        {
            T Data = data;
            Success = success;
            HttpCode = httpCode;

            return this;
        }

        public ServiceResponse<T> Response(string message, bool success = true)
        {
            Success = success;
            Message = message;

            return this;
        }

        public ServiceResponse<T> Response(HttpStatusCode httpCode,
            string message,
            bool success = true)
        {
            HttpCode = httpCode;
            Success = success;
            Message = message;

            return this;
        }

        public ServiceResponse<T> Response(T data,
            HttpStatusCode httpCode,
            string message = "",
            bool success = true)
        {
            Data = data;
            HttpCode = httpCode;
            Success = success;
            Message = message;

            return this;
        }
    }
}