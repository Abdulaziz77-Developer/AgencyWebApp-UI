using Microsoft.AspNetCore.Components;
using TravelWebApp.Infrastrcuture.api.auth;

namespace TravelWebApp.Features.Auth
{
    public partial class Register : ComponentBase
    {
        [Inject] public AuthService AuthService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private string email = string.Empty;
        private string phone = string.Empty;
        private string password = string.Empty;
        private string confirmPassword = string.Empty;

        private string? errorMessage;
        private string? successMessage;
        private bool isLoading = false;

        private async Task RegisterUser()
        {
            errorMessage = null;
            successMessage = null;

            if (password != confirmPassword)
            {
                errorMessage = "Пароли не совпадают";
                return;
        }

        isLoading = true;

        var fullName = $"{firstName} {lastName}";
        var success = await AuthService.RegisterAsync(fullName, email, password);

        isLoading = false;

        if (success)
        {
            successMessage = "Регистрация прошла успешно! Перенаправление на страницу входа...";
            await Task.Delay(1200);
            Navigation.NavigateTo("/login");
        }
        else
        {
            errorMessage = "Регистрация не удалась. Попробуйте другой адрес электронной почты.";
        }
    }
    }
}