using System.Net;

namespace Core.Shared;

public class Response<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
