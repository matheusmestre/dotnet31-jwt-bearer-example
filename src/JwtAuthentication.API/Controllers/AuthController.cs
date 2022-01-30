using JwtAuthentication.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthentication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult TokenAsync([FromBody] AuthRequest request)
        {
            if (!request.IsValid())
                return BadRequest();

            if (request.UserName == "admin"
                && request.Password == "admin")
            {
                var model = GenerateAuthentication(request);
                return Ok(model);
            }

            return Unauthorized();
        }
        private AuthResponse GenerateAuthentication(AuthRequest request)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecuritySettings.AccessTokenSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserName)
            };

            var expiration = SecuritySettings.AccessTokenExpiration.TotalMinutes;

            var token = new JwtSecurityToken(
                SecuritySettings.Issuer,
                SecuritySettings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expiration),
                credentials
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponse
            {
                AccessToken = accessToken,
            };
        }
    }
}