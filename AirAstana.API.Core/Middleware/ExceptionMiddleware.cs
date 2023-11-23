using System.Net;
using AirAstana.API.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AirAstana.API.Core.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var errorDetails = new ErrorDetails()
        {
            ErrorType = "Failure",
            ErrorMessage = exception.Message
        };
        
        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails.ErrorType = "Not Found";
                break;
        }
        
        var response = JsonConvert.SerializeObject(errorDetails);
        httpContext.Response.StatusCode = (int) statusCode;
        return httpContext.Response.WriteAsync(response);
    }
}

public class ErrorDetails
{
    public string ErrorType { get; set; } = "Failure";
    public string ErrorMessage { get; set; } = "Something went wrong";
}