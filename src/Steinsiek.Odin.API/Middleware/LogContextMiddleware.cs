namespace Steinsiek.Odin.API.Middleware;

/// <summary>
/// Middleware that enriches the Serilog LogContext with per-request properties.
/// </summary>
public sealed class LogContextMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogContextMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public LogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Enriches the log context with the request path, HTTP method, and correlation ID.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        using (LogContext.PushProperty("RequestMethod", context.Request.Method))
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            await _next(context);
        }
    }
}

/// <summary>
/// Extension methods for registering the log context middleware.
/// </summary>
public static class LogContextMiddlewareExtensions
{
    /// <summary>
    /// Adds the log context enrichment middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder for chaining.</returns>
    public static IApplicationBuilder UseLogContext(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LogContextMiddleware>();
    }
}
