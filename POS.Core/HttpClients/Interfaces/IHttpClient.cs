namespace POS.Infrastructure.HttpClients.Interfaces;

public interface IHttpClient
{
    Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
}
