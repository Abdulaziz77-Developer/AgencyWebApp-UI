using System.Security.Claims;

namespace TravelWebApp.Auth
{
    public class AuthState
    {
        public string? Token { get; set; }
        public int UserId { get; set; }
        public ClaimsPrincipal User { get; set; }
            = new ClaimsPrincipal(new ClaimsIdentity());
    }

}
