using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Demo.Services
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}