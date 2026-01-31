using System.Configuration;

namespace POS.Infrastructure.Config;

public class AppConfig : IAppConfig
{
    public string Get(string key)
    {
        return ConfigurationManager.AppSettings[key]
            ?? throw new ConfigurationErrorsException(
                $"Missing key '{key}' in app.config");
    }

    public T Get<T>(string key)
    {
        var value = Get(key);
        return (T)Convert.ChangeType(value, typeof(T));
    }
}
