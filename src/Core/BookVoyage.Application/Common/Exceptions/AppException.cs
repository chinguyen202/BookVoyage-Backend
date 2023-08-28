namespace BookVoyage.Application.Common.Exceptions;

/// <summary>
/// Standardized exception objects with status code, message, and details.
/// </summary>
public class AppException
{
    public AppException(int statusCode, string message, string details = null)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}