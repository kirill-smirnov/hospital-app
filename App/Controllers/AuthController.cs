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

            public bool IsStaff { get; set; }
        }

        private ActionResult<object> CreateUserResponse(string id, string token)
        {
            Person user = DataUtilsService.GetPerson(id);

            return new
            {
                access_token = token,
                id = id,
                username = user.LoginInfo.Username,
                role = Enum.GetName(typeof(Role), user.Role)
            };
        }

        [HttpPost("login")]
        public ActionResult<object> GetToken(LoginModel model)
        {
            var identity = AuthService.GetIdentity(model.Username, model.Password, model.IsStaff);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var encodedJwt = AuthService.GenerateToken(identity);

            var id = identity.Name;

            return CreateUserResponse(id, encodedJwt);
        }

        public class TokenModel
        {
            public string Token { get; set; }
        }

        public class TokenWithIdModel: TokenModel
        {
            public string Id { get; set; }
        }

        [HttpPost("login/cookies")]
        public ActionResult<object> GetUserByToken(TokenWithIdModel model)
        {
            string token = model.Token;
            bool isValid = AuthService.ValidateToken(token);

            if (isValid)
                return CreateUserResponse(model.Id, token);

            return new { isValid = false };
        }

        [HttpPost("token/validate")]
        public ActionResult<object> ValidateToken(TokenModel tokenModel)
        {
            bool isValid = AuthService.ValidateToken(tokenModel.Token);

            return new { isValid = isValid };
        }
    }
}
