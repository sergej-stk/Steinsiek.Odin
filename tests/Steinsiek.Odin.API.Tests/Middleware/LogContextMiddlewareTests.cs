namespace Steinsiek.Odin.API.Tests.Middleware;

/// <summary>
/// Unit tests for <see cref="LogContextMiddleware"/>.
/// </summary>
[TestClass]
public sealed class LogContextMiddlewareTests
{
    [TestMethod]
    public async Task InvokeAsync_ShouldCallNextDelegate()
    {
        // Arrange
        var wasCalled = false;
        var middleware = new LogContextMiddleware(_ => { wasCalled = true; return Task.CompletedTask; });
        var context = new DefaultHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.IsTrue(wasCalled);
    }

    [TestMethod]
    public async Task InvokeAsync_ShouldNotThrow_WhenNextSucceeds()
    {
        // Arrange
        var middleware = new LogContextMiddleware(_ => Task.CompletedTask);
        var context = new DefaultHttpContext();

        // Act & Assert — no exception expected
        await middleware.InvokeAsync(context);
    }
}
