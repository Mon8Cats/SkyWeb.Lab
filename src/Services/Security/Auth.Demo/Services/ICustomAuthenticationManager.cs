using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Demo.Services
{
    public interface ICustomAuthenticationManager
    {
        string Authenticate(string username, string password);
        public IDictionary<string, string> Tokens { get; }
    }
}