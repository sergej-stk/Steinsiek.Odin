namespace Steinsiek.Odin.API.Tests.Middleware;

/// <summary>
/// Unit tests for <see cref="ExceptionHandlingMiddleware"/>.
/// </summary>
[TestClass]
public sealed class ExceptionHandlingMiddlewareTests
{
    private Mock<ILogger<ExceptionHandlingMiddleware>> _loggerMock = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
    }

    private ExceptionHandlingMiddleware CreateMiddleware(RequestDelegate next)
    {
        return new ExceptionHandlingMiddleware(next, _loggerMock.Object);
    }

    private static DefaultHttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext();
        context.Response.Body = new System.IO.MemoryStream();
        return context;
    }

    private static async Task<string> ReadResponseBody(DefaultHttpContext context)
    {
        context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
        using var reader = new System.IO.StreamReader(context.Response.Body);
        return await reader.ReadToEndAsync();
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldPassThrough_WhenNoException()
    {
        // Arrange
        var wasCalled = false;
        var middleware = CreateMiddleware(_ => { wasCalled = true; return Task.CompletedTask; });
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.IsTrue(wasCalled);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn404_ForElementNotFoundException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new ElementNotFoundException("Not found"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status404NotFound, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn400_ForOdinValidationException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new OdinValidationException("Invalid"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn400_ForBusinessRuleException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new BusinessRuleException("Rule violated"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn400_ForArgumentException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new ArgumentException("Bad argument"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status400BadRequest, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn404_ForKeyNotFoundException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new KeyNotFoundException("Key not found"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status404NotFound, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn401_ForUnauthorizedAccessException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new UnauthorizedAccessException());
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldReturn500_ForUnexpectedException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new InvalidOperationException("Unexpected"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldSetJsonContentType()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new InvalidOperationException("Error"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual("application/json", context.Response.ContentType);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldWriteCamelCaseJsonBody()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new ElementNotFoundException("Not found"));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);
        var body = await ReadResponseBody(context);

        // Assert
        StringAssert.Contains(body, "\"statusCode\"");
        StringAssert.Contains(body, "\"message\"");
        StringAssert.Contains(body, "\"errorType\"");
        StringAssert.Contains(body, "\"timestamp\"");
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldUseCustomStatusCode_ForOdinException()
    {
        // Arrange
        var middleware = CreateMiddleware(_ => throw new OdinException("Custom", 422, OdinErrorType.Validation));
        var context = CreateHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.AreEqual(422, context.Response.StatusCode);
    }
}
