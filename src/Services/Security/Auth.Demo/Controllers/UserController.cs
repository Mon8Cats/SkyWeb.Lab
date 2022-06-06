using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Demo.Models;
using Auth.Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ICustomAuthenticationManager customAuthenticationManager;

        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly ITokenRefresher tokenRefresher;

        public UserController(IJwtAuthenticationManager jwtAuthenticationManager, ITokenRefresher tokenRefresher)
        //public UserController(ICustomAuthenticationManager customAuthenticationManager)
        {
            //this.customAuthenticationManager = customAuthenticationManager;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.tokenRefresher = tokenRefresher;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name="Get")]
        public string Get(int id)
        {
            return "value";
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            //var token = customAuthenticationManager.Authenticate(userCred.Username, userCred.Password);

            if( token == null)
                return Unauthorized();

            return Ok(token);
        }

        [AllowAnonymous]
         [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshCred refreshCred)
        {
            var token = tokenRefresher.Refresh(refreshCred);

            if( token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}