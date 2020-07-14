using Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
    public class AuthService : IAuthService
    {
        protected IDataUtilsService DataUtilsService { get; }
        public AuthService(IDataUtilsService service)
        {
            DataUtilsService = service;
        }
        public ClaimsIdentity GetIdentity(string username, string password, string _role)
        {
            var role = (Role)Enum.Parse(typeof(Role), _role);

            return GetIdentity(username, password, role);
        }

        public ClaimsIdentity GetIdentity(string username, string password, Role role)
        {
            Person person = role == Role.Patient ?
                DataUtilsService.GetPatient(username, password):
                DataUtilsService.GetEmployee(username, password, role);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
    }
    }
}
