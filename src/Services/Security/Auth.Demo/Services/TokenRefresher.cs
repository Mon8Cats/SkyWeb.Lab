using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Demo.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Demo.Services
{

    public class TokenRefresher : ITokenRefresher
    {
        //private readonly byte[] key;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        private readonly string key;

        public TokenRefresher(string key, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.key = key;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            //this.key = key;
        }
        public AuthenticationResponse Refresh(RefreshCred refreshCred)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;

            var principal = tokenHandler.ValidateToken(
                refreshCred.JwtToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                },
                out validatedToken
            );

            var jwtToken = validatedToken as JwtSecurityToken;

            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid tokne passed!");
            }

            var userName = principal.Identity.Name;
            if(refreshCred.RefreshToken != jwtAuthenticationManager.UsersRefreshTokens[userName])
            {
                throw new SecurityTokenException("Invalid tokne passed!");
            }

            return jwtAuthenticationManager.Authenticate(userName, principal.Claims.ToArray());

        }
    }
}