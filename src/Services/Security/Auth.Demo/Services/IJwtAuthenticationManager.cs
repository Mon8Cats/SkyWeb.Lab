using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Demo.Models;

namespace Auth.Demo.Services
{
    public interface IJwtAuthenticationManager
    {
        AuthenticationResponse Authenticate(string username, string password);
        AuthenticationResponse Authenticate(string username, Claim[] claims);
        IDictionary<string, string> UsersRefreshTokens { get; }
    }
}