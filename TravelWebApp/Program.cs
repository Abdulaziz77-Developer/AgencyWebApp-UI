//using Blazored.LocalStorage;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using TravelWebApp.Auth;
//using TravelWebApp.Components;
//using TravelWebApp.Extensions;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents().AddInteractiveServerComponents();
//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri("http://localhost:5188/")
//});
//builder.Services.AddApplicationServices();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddHttpClient();
//builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<AuthState>();
//builder.Services.AddScoped<AuthMessageHandler>();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<CustomAuthStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
//builder.Services.AddAuthorization();
////builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
////    .AddCookie();

//builder.Services.AddAuthentication(options => {
//    options.DefaultScheme = "Cookies";
//}).AddCookie("Cookies");
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseHttpsRedirection();

//app.UseStaticFiles();
//app.UseAntiforgery();

//app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

//app.Run();
// ... ���� using ...

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TravelWebApp.Auth;
using TravelWebApp.Components;
using TravelWebApp.Extensions;
using TravelWebApp.Services.Implementations;
using TravelWebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. ��������� ����������
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. �������� ��� .NET 8: ��������� ��������� ��������������
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.81:5180") });

builder.Services.AddApplicationServices();
builder.Services.AddHttpClient();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IOtpService, OtpService>();

// 3. ���������� ����������� ���������� (���� ��������� �� ����)
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthMessageHandler>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = "Cookies";
}).AddCookie("Cookies");

var app = builder.Build();

// ... ��������� ��� (Middlewares) ...
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery(); // ����������� ����� MapRazorComponents

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();