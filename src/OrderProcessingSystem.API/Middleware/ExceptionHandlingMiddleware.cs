using System.Net;
using System.Text.Json;

namespace OrderProcessingSystem.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                errors = exception.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage
                })
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
        catch (ArgumentException exception)
        {
            await HandleExceptionAsync(
                context,
                exception,
                HttpStatusCode.BadRequest
            );
        }
        catch (InvalidOperationException exception)
        {
            await HandleExceptionAsync(
                context,
                exception,
                HttpStatusCode.BadRequest
            );
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception occured");

            await HandleExceptionAsync(
                context,
                exception,
                HttpStatusCode.InternalServerError,
                "Internal server error"
            );
        }
    }
    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode,
        string? message = null
    )
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = message ?? exception.Message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}