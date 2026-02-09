namespace Steinsiek.Odin.API.Middleware;

/// <summary>
/// Middleware for handling unhandled exceptions and returning standardized error responses.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware and handles any exceptions.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        var (statusCode, message, errorType) = exception switch
        {
            ElementNotFoundException ex => (StatusCodes.Status404NotFound, ex.Message, OdinErrorType.NotFound),
            OdinValidationException ex => (StatusCodes.Status400BadRequest, ex.Message, OdinErrorType.Validation),
            BusinessRuleException ex => (StatusCodes.Status400BadRequest, ex.Message, OdinErrorType.BusinessRule),
            OdinException ex => (ex.StatusCode, ex.Message, ex.ErrorType),
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message, OdinErrorType.Validation),
            KeyNotFoundException => (StatusCodes.Status404NotFound, exception.Message, OdinErrorType.NotFound),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access", OdinErrorType.Unauthorized),
            _ => (StatusCodes.Status500InternalServerError, "An internal error occurred", OdinErrorType.Internal)
        };

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = statusCode;

        var errorDetails = new ErrorDetails
        {
            StatusCode = statusCode,
            Message = message,
            ErrorType = errorType,
            Timestamp = DateTime.UtcNow
        };

        var result = JsonSerializer.Serialize(errorDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(result);
    }
}

/// <summary>
/// Extension methods for registering the exception handling middleware.
/// </summary>
public static class ExceptionHandlingMiddlewareExtensions
{
    /// <summary>
    /// Adds the exception handling middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder for chaining.</returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
