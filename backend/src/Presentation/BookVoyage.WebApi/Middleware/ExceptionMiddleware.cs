using System.Net;
using System.Text.Json;

using BookVoyage.Application.Common.Exceptions;

namespace BookVoyage.WebApi.Middleware;


/// <summary>
/// Custom middleware responsible for catching unhandled exceptions that might occur during the request processing pipeline
/// and responding with an appropriate error message in JSON format. 
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = _hostEnvironment.IsDevelopment()
                ? new AppException(context.Response.StatusCode, e.Message, e.StackTrace?.ToString())
                : new AppException(context.Response.StatusCode, e.Message, "Internal Server Error");
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}