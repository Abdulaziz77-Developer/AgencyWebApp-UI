using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TravelWebApp.Infrastrcuture.api.auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        // ВАЖНО: Поле для хранения текущего пользователя
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task<string> GetUserEmail()
        {
            // Получаем текущее состояние авторизации
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not { IsAuthenticated: true })
            {
                return string.Empty;
            }

            // Пробуем найти email в стандартных клаймах или в кастомном поле "email"
            var emailClaim = user.FindFirst(c => c.Type == ClaimTypes.Email)
                          ?? user.FindFirst(c => c.Type == "email");

            return emailClaim?.Value ?? string.Empty;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                Console.WriteLine(token);
                if (string.IsNullOrWhiteSpace(token))
                {
                    _currentUser = _anonymous;
                    return new AuthenticationState(_anonymous);
                }

                var identity = GetIdentityFromToken(token);

                // Проверка на валидность (не просрочен ли токен)
                var expiryClaim = identity.FindFirst(exp => exp.Type == JwtRegisteredClaimNames.Exp);
                if (expiryClaim != null)
                {
                    var expiryTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryClaim.Value));
                    if (expiryTime <= DateTimeOffset.Now)
                    {
                        await MarkUserAsLoggedOut();
                        return new AuthenticationState(_anonymous);
                    }
                }

                _currentUser = new ClaimsPrincipal(identity);
                return new AuthenticationState(_currentUser);
            }
            catch
            {
                _currentUser = _anonymous;
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            var identity = GetIdentityFromToken(token);

            _currentUser = new ClaimsPrincipal(identity);

            var userId = identity.FindFirst("nameid")?.Value ?? identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = identity.FindFirst(ClaimTypes.Role)?.Value;

            await _localStorage.SetItemAsync("UserId", userId);
            await _localStorage.SetItemAsync("Role", role);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public int? GetUserId()
        {
            // Теперь _currentUser будет содержать данные после вызова GetAuthenticationStateAsync
            if (_currentUser.Identity?.IsAuthenticated != true)
                return null;

            var userIdClaim = _currentUser.FindFirst(ClaimTypes.NameIdentifier)
                           ?? _currentUser.FindFirst("nameid")
                           ?? _currentUser.FindFirst("sub");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int id))
            {
                return id;
            }

            return null;
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("UserId");
            await _localStorage.RemoveItemAsync("Role");

            _currentUser = _anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private ClaimsIdentity GetIdentityFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claims = jwt.Claims.ToList();

            var roleClaims = jwt.Claims.Where(c =>
                c.Type.Equals("role", StringComparison.OrdinalIgnoreCase) ||
                c.Type == ClaimTypes.Role).ToList();

            foreach (var roleClaim in roleClaims)
            {
                string normalizedValue = roleClaim.Value.Equals("admin", StringComparison.OrdinalIgnoreCase) ? "Admin" :
                                         roleClaim.Value.Equals("user", StringComparison.OrdinalIgnoreCase) ? "User" :
                                         roleClaim.Value;

                if (!claims.Any(c => c.Type == ClaimTypes.Role && c.Value == normalizedValue))
                {
                    claims.Add(new Claim(ClaimTypes.Role, normalizedValue));
                }
            }

            return new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
        }
    }
}