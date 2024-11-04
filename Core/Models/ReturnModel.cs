namespace Core.Models;

public class ReturnModel<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }

    public string Message { get; set; }

    public int StatusCode { get; set; }

    public ReturnModel(T data, bool success = true, string message = null, int statusCode = 200)
    {
        Data = data;
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }
}