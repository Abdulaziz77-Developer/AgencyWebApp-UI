using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides; // Добавлено для Docker
using TravelWebApp.Auth;
using TravelWebApp.Components;
using TravelWebApp.Extensions;
using TravelWebApp.Services.Implementations;
using TravelWebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Настройка компонентов
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

// 2. Умная настройка HttpClient для Docker
// Если приложение видит, что оно в контейнере, оно использует имя сервиса, иначе - localhost
// var backendUrl = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true" 
//                  ? "http://backend-container:8080" 
//                  : "http://localhost:8080";
var backendUrl = "http://192.168.1.81:5180";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrl) });

// 3. Настройка заголовков для работы за прокси (Docker)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddApplicationServices();
builder.Services.AddHttpClient();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IOtpService, OtpService>();

// 4. Авторизация
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client => 
{
    client.BaseAddress = new Uri(backendUrl);
});

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthMessageHandler>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = "Cookies";
}).AddCookie("Cookies");

var app = builder.Build();

// ПОРЯДОК MIDDLEWARE КРИТИЧЕСКИ ВАЖЕН
app.UseForwardedHeaders(); // Должно быть первым

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// app.UseHttpsRedirection(); // В Docker часто мешает, если не настроены сертификаты

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();