using System.Net.Http.Headers;
using TravelWebApp.Infrastrcuture.api.auth;


namespace TravelWebApp.Infrastrcuture.api.handlers
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly AuthState _authState;

        public AuthMessageHandler(AuthState authState)
        {
            _authState = authState;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_authState.Token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authState.Token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}