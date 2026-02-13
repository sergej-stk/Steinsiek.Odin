namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Attaches the JWT Bearer token to outgoing API requests.
/// </summary>
public sealed class ApiAuthenticationHandler(ITokenStorageService tokenStorage) : DelegatingHandler
{
    private readonly ITokenStorageService _tokenStorage = tokenStorage;

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetToken(cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
