﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Services
{
    public interface IAuthService
    {
        ClaimsIdentity GetIdentity(string username, string password, bool isStaff);
        public string GenerateToken(ClaimsIdentity identity);
        public bool ValidateToken(string token);
    }
}
