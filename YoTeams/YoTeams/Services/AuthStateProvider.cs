using Microsoft.AspNetCore.Components;
using YoTeams.Models;

namespace YoTeams.Services;

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;

    public AuthStateProvider(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine("GetAuthenticationStateAsync triggered!");

        try
        {
            var response = await _httpClient.GetAsync("auth/validate");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); 
            }

            if (!response.IsSuccessStatusCode)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var authData = await response.Content.ReadFromJsonAsync<AuthValidateResponse>();

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, authData.Username)
            }, "jwt");

            identity.AddClaims(authData.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var user = new ClaimsPrincipal(identity);
            Console.WriteLine($" User authenticated: {authData.Username}");
            return new AuthenticationState(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error in GetAuthenticationStateAsync: {ex.Message}");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public void NotifyUserAuthentication(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToList();

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }
}
