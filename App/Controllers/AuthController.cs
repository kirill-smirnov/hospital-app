using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace App.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDataStorage DataStorage;
        private readonly IAuthService AuthService;

        public AuthController(IDataStorage dataStorage, IAuthService authService)
        {
            DataStorage = dataStorage;
            AuthService = authService;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public string Role { get; set; }
        }

        [HttpPost("token")]
        public ActionResult<object> GetToken(LoginModel model)
        {
            var identity = AuthService.GetIdentity(model.Username, model.Password, model.Role);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
        }
    }
}
