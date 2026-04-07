using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using TravelWebApp.Infrastrcuture.api.auth;

namespace TravelWebApp.Features.Auth
{
    public partial class Login : ComponentBase 
    {
    private string email = string.Empty;
    private string password = string.Empty;
    private string? errorMessage;
    private bool isLoading = false;
    
    [Inject] public AuthService AuthService { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
    private async Task HandleLogin()
    {
        errorMessage = null;
        isLoading = true;

        // Пытаемся залогиниться через AuthService
        var success = await AuthService.LoginAsync(email, password);

        isLoading = false;

        if (!success)
        {
            errorMessage = "Invalid email or password";
            return;
        }

        // Получаем текущее состояние аутентификации
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            errorMessage = "Пользователь не аутентифицирован после входа. Пожалуйста, попробуйте снова.";
            return;
        }

        // Получаем роль пользователя из claims (ClaimTypes.Role или "role")
        var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
        Console.WriteLine("User role: " + role);

        // Навигация в зависимости от роли
        if (!string.IsNullOrEmpty(role) && role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            Navigation.NavigateTo("/admindashboard");
        }
        else if(!string.IsNullOrEmpty(role) && role.Equals("User",StringComparison.OrdinalIgnoreCase))
        {
            Navigation.NavigateTo("/userdashboard");
        }
        else
        {
            Navigation.NavigateTo("/");
        }
    }
    }
}