
using POS.Infrastructure.HttpClients.Interfaces;

namespace POS.Infrastructure.HttpClients;

public class DefaultHttpClient : IHttpClient
{
    private readonly HttpClient _client = new HttpClient();

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        => _client.PostAsync(url, content);
}