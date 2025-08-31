namespace Bosta.API.Services.ApiCall;
public interface IApiCall
{
    Task<TResponse?> GetAsync<TResponse>(string url, IDictionary<string, string>? headers = null);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body, IDictionary<string, string>? headers = null);
    Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest body, IDictionary<string, string>? headers = null);
    Task<TResponse?> DeleteAsync<TResponse>(string url, IDictionary<string, string>? headers = null);
}
