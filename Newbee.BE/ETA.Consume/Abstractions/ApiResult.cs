using System;
namespace ETA.Consume.Abstractions;

public class ApiResult<TData>
{
    public bool IsSuccess { get; set; }
    public TData? Data { get; set; }
    public ApiError Error { get; set; } = default!;
    public int StatusCode { get; set; }

    public static ApiResult<TData> Success(TData data,int statusCode)
    {
        return new ApiResult<TData>()
        {
            IsSuccess = true,
            Data = data,
            StatusCode = statusCode
        };
    }

    public static ApiResult<TData> Failure(ApiError error,int statusCode)
    {
        return new ApiResult<TData>()
        {
            IsSuccess = false,
            Error = error,
            StatusCode = statusCode
        };
    }
}
