using Auth.Demo.Models;

namespace Auth.Demo.Services
{
    public interface ITokenRefresher
    {
        AuthenticationResponse Refresh(RefreshCred refreshCred);
    }
}