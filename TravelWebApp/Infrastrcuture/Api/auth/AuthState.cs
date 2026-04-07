using System.Security.Claims;

namespace TravelWebApp.Infrastrcuture.api.auth
{
    public class AuthState
    {
        public string? Token { get; set; }
        public int UserId { get; set; }
        public ClaimsPrincipal User { get; set; }
            = new ClaimsPrincipal(new ClaimsIdentity());
    }

}
