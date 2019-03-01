using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WeatherApp.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace WeatherApp.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string CreateToken(string name, string password, string role)
        {
                string securityKey = _jwtSettings.Key;
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, role));

                DateTime currentDate = DateTime.UtcNow;
                DateTime expires = currentDate.AddMinutes(_jwtSettings.ExpiryMinutes);
                var token = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        claims: claims,
                        notBefore: currentDate,
                        expires: expires,
                        signingCredentials: signingCredentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
          
        }
    }
}
