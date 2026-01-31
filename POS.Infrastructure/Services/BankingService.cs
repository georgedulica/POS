using POS.Models;
using POS.Infrastructure.Config;
using POS.Core.Interfaces;
using System.Text;
using Newtonsoft.Json;
using POS.Infrastructure.HttpClients;
using POS.Infrastructure.HttpClients.Interfaces;

namespace POS.Core.Services;

public class BankingService : IBankingService
{
    private readonly IAppConfig _config;
    private readonly IHttpClient _httpClient;

    public BankingService()
    {
        _config = new AppConfig();
        _httpClient = new DefaultHttpClient();
    }

    public async Task<string?> ProcessPayment(ValidatedData validatedData)
    {
        try
        {
            var url = _config.Get("BankingServiceUrl");
            var json = JsonConvert.SerializeObject(validatedData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(responseJson);
            }

            Console.WriteLine($"Payment failed with status code: {response.StatusCode}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}