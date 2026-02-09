namespace Steinsiek.Odin.API.Tests;

/// <summary>
/// Shared test setup that initializes a single web application factory for all integration tests.
/// </summary>
[TestClass]
public static class TestSetup
{
    /// <summary>
    /// Gets the shared web application factory.
    /// </summary>
    internal static OdinWebApplicationFactory Factory { get; private set; } = null!;

    /// <summary>
    /// Gets the shared HTTP client for API requests.
    /// </summary>
    internal static HttpClient Client { get; private set; } = null!;

    /// <summary>
    /// Initializes the test server once before all tests in the assembly.
    /// </summary>
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext _)
    {
        Factory = new OdinWebApplicationFactory();
        Client = Factory.CreateClient();
    }

    /// <summary>
    /// Disposes test resources after all tests in the assembly.
    /// </summary>
    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}
