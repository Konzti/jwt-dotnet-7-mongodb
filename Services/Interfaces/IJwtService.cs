using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDotNet7.Models;

namespace JwtDotNet7.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwt(User user);
    }
}