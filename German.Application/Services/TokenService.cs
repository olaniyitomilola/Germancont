using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using German.Core.Entities;
using German.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace German.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Author author)
        {
                var claims = new[]
            {
                // "sub" claim is typically
                // //used to identify the subject of the JWT,
                new Claim(JwtRegisteredClaimNames.Sub, author.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, author.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(30),
                    signingCredentials: creds
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
       
}
