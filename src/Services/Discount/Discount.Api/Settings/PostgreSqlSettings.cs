using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Settings
{
    public class PostgreSqlSettings
    {
        public string Host { get; init;}
        public int Port { get; init;}

        public string DatabaseName { get; init; }
        public string UserId { get; init; }
        public string Password { get; init; }
        public string ConnectionString => $"Server={Host};Port={Port};Database={DatabaseName};User Id={UserId};Password={Password};";
    }
}