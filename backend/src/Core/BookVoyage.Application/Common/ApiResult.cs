namespace BookVoyage.Application.Common;

public class ApiResult<T>
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; }
    public string Error { get; set; }

    public static ApiResult<T> Success(T value) => new ApiResult<T> { IsSuccess = true, Value = value };
    public static ApiResult<T> Failure(string error) => new ApiResult<T> { IsSuccess = false, Error = error };
}