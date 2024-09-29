using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Louvryy.Core.Tests.MinimalApi.Configurations;

public static class AuthenticationConfiguration
{

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddScheme<ApiKeySchemeOptions, ApiKeyScheme>("api-key", cfg => { });

        return services;
    }
}

public class ApiKeySchemeOptions : AuthenticationSchemeOptions { }

public class ApiKeyScheme : AuthenticationHandler<ApiKeySchemeOptions>
{
    public ApiKeyScheme(
        IOptionsMonitor<ApiKeySchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    ) : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.Request.Headers["X-API-KEY"] != "lorem") {
            return await Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }

        var claims = new[] { new Claim(ClaimTypes.Name, "Test") };
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}