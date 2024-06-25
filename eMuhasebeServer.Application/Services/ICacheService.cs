namespace eMuhasebeServer.Application.Services;

public interface 
    ICacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan? expire = null);
    bool Remove(string key);
    void RemoveAll();
}