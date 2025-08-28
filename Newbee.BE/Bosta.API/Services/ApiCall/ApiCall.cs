using Bosta.API;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Bosta.API.Services.ApiCall
{
    public class ApiCall : IApiCall
    {
        private readonly HttpClient _httpClient;

        public ApiCall(HttpClient client, IOptions<BostaAppSettings> options)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        #region HTTP Methods

        public async Task<TResponse?> GetAsync<TResponse>(string url,IDictionary<string, string>? headers = null)
        {
            var request = BuildRequest<object>(url, HttpMethod.Get, headers, null);

            var response = await _httpClient.SendAsync(request);
            await EnsureSuccess(response);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url,TRequest body,IDictionary<string, string>? headers = null)
        {
            var request = BuildRequest(url, HttpMethod.Post, headers, body);

            var response = await _httpClient.SendAsync(request);
            await EnsureSuccess(response);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url,TRequest body,IDictionary<string, string>? headers = null)
        {
            var request = BuildRequest(url, HttpMethod.Put, headers, body);

            var response = await _httpClient.SendAsync(request);
            await EnsureSuccess(response);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse?> DeleteAsync<TResponse>(string url,IDictionary<string, string>? headers = null)
        {
            var request = BuildRequest<string>(url, HttpMethod.Delete, headers, null);

            var response = await _httpClient.SendAsync(request);
            await EnsureSuccess(response);

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        #endregion

        #region Build Request
        private HttpRequestMessage BuildRequest<T>(string url,HttpMethod method,IDictionary<string, string>? headers,T? body)
        {
            var request = new HttpRequestMessage(method, url);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (body != null && method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return request;
        }
        #endregion

        #region Error Handling
        private async Task EnsureSuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var responseBody = await response.Content.ReadAsStringAsync();

            throw response.StatusCode switch
            {
                HttpStatusCode.BadRequest => new InvalidOperationException($"Bad Request: {responseBody}"),
                HttpStatusCode.Unauthorized => new UnauthorizedAccessException($"Unauthorized: {responseBody}"),
                HttpStatusCode.Forbidden => new UnauthorizedAccessException($"Forbidden: {responseBody}"),
                HttpStatusCode.NotFound => new KeyNotFoundException($"Not Found: {responseBody}"),
                HttpStatusCode.InternalServerError => new Exception($"Server Error: {responseBody}"),
                _ => new Exception($"Unexpected Error ({(int)response.StatusCode}): {responseBody}")
            };
        }
        #endregion
    }


}
