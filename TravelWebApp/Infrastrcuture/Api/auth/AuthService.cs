using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace TravelWebApp.Infrastrcuture.api.auth;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly CustomAuthStateProvider? _authProvider;

    public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        // Используем 'as' для безопасного приведения
        _authProvider = authStateProvider as CustomAuthStateProvider;
    }

    // LOGIN
    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", new { email, password });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result == null || string.IsNullOrEmpty(result.Token))
                return false;

            if (_authProvider != null)
            {
                await _authProvider.MarkUserAsAuthenticated(result.Token);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    // REGISTER
    public async Task<bool> RegisterAsync(string fullName, string email, string password)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/Auth/register", new { fullName, email, password });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result != null && !string.IsNullOrEmpty(result.Token) && _authProvider != null)
            {
                await _authProvider.MarkUserAsAuthenticated(result.Token);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    // LOGOUT
    public async Task LogoutAsync()
    {
        if (_authProvider != null)
        {
            await _authProvider.MarkUserAsLoggedOut();
        }
    }
}

public class LoginResponse
{
    public string Token { get; set; } = null!;
}