namespace Grbus.WebShop.Application.Common
{
    public interface ICachingService
    {
        bool TryGet<T>(string key, out T? value);
        Task Set<T>(string key, T value);
        Task Remove(string key);
    }
}
