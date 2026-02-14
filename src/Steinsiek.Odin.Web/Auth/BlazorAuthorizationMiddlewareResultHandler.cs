namespace Steinsiek.Odin.Web.Auth;

/// <summary>
/// Custom authorization middleware result handler that passes through to Blazor's
/// <c>AuthorizeRouteView</c> instead of issuing HTTP-level challenges.
/// </summary>
/// <remarks>
/// In Blazor Server, authorization is handled at the component level by
/// <c>AuthorizeRouteView</c>, which shows a <c>NotAuthorized</c> template.
/// The default <see cref="IAuthorizationMiddlewareResultHandler"/> calls
/// <c>ChallengeAsync</c>, which requires a configured authentication scheme.
/// This handler bypasses that and lets Blazor handle authorization display.
/// </remarks>
public sealed class BlazorAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    /// <inheritdoc />
    public Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        return next(context);
    }
}
