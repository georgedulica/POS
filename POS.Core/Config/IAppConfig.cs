namespace POS.Infrastructure.Config;

public interface IAppConfig
{
    string Get(string key);

    T Get<T>(string key);
}