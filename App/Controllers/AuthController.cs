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
        private readonly IDataUtilsService DataUtilsService;

        public AuthController(IDataStorage dataStorage, IAuthService authService, IDataUtilsService dataUtilsService)
        {
            DataStorage = dataStorage;
            AuthService = authService;
            DataUtilsService = dataUtilsService;
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

            var encodedJwt = AuthService.GenerateToken(identity);

            var id = identity.Name;
            string role = Enum.GetName(typeof(Role), Role.Patient);

            Person user = model.Role == role ?
                (Person)DataUtilsService.GetPatient(id) :
                (Person)DataUtilsService.GetDoctor(id);

            return new
            {
                access_token = encodedJwt,
                id = id,
                username = user.LoginInfo.Username,
                role = role
            };
        }
    }
}
