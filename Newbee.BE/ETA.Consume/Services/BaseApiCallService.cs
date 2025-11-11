using ETA.Consume.Abstractions;
using ETA.Consume.Extensions;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ETA.Consume.Services;

public class BaseApiCallService
{
    protected readonly HttpClient _client;
    public BaseApiCallService(HttpClient httpClient)
    {
        _client = httpClient;
    }
    public void Dispose()
    {
        _client?.Dispose();
    }
    private  HttpRequestMessage BuildRequest<TContent>(string url,HttpMethod httpMethod,TContent? content, Dictionary<string, string>? headers)
    {
        var request = new HttpRequestMessage(httpMethod, url);

        if (content is null)
        {
            request.Content = null;
        }
        else if (content is HttpContent httpContent)
        {
            request.Content = httpContent;
        }
        else
        {
            var json = JsonConvert.SerializeObject(content,JsonSettingsSerializer.Options);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        return request;
    }

    public async Task<ApiResult<TResponse>> PostReturnAsync<TContent,TResponse>(string url, TContent? content, Dictionary<string, string>? headers = null)
    {
        using var request = BuildRequest(url,HttpMethod.Post,content, headers);
        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        if(response.IsSuccessStatusCode)
        {
            return ApiResult<TResponse>.Success(JsonConvert.DeserializeObject<TResponse>(responseContent)!,(int)response.StatusCode);
        }
        else
        {
            return ApiResult<TResponse>.Failure(JsonConvert.DeserializeObject<ApiError>(responseContent)!,(int)response.StatusCode);
        }
    }

    public async Task<ApiResult<TResponse>> GetAsync<TResponse>(string url,Dictionary<string,string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);

        if(headers != null)
        {
            foreach(var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode ? 
            ApiResult<TResponse>.Success(JsonConvert.DeserializeObject<TResponse>(responseContent)!,(int)response.StatusCode) :
            ApiResult<TResponse>.Failure(JsonConvert.DeserializeObject<ApiError>(responseContent)!, (int)response.StatusCode);
    }


}
