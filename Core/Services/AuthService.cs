using Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services
{
    public class AuthOptions
    {
        public string Issuer { get; set; } 
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int LifetimeInMinutes = 1;
    }

    public class AuthService : IAuthService
    {
        protected IDataUtilsService DataUtilsService { get; }
        protected AuthOptions AuthOptions { get; }
        public AuthService(IDataUtilsService service, AuthOptions authOptions)
        {
            DataUtilsService = service;
            AuthOptions = authOptions;
        }
        public ClaimsIdentity GetIdentity(string username, string password, string _role)
        {
            var role = (Role)Enum.Parse(typeof(Role), _role);

            Person person = role == Role.Patient ?
                DataUtilsService.GetPatient(username, password) :
                DataUtilsService.GetEmployee(username, password, role);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.LoginInfo.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

        public string GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var symmetricKey = AuthService.GetSymmetricSecurityKey(AuthOptions.SecretKey);
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifetimeInMinutes)),
                    signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }
    }
}
