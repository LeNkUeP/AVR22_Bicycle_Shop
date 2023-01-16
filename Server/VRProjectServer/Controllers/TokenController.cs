using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace VRProjectServer.Controllers
{
    [Route("generatetoken")]
    public class TokenController : Controller
    {
        [HttpGet]
        public string GenerateToken()
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, Request.Query["user"]) };
            var credentials = new SigningCredentials(SecurityConfig.SecurityKey, SecurityAlgorithms.HmacSha256); // Too lazy to inject the key as a service
            var token = new JwtSecurityToken("SignalRTestServer", "SignalRTests", claims, expires: DateTime.UtcNow.AddSeconds(30), signingCredentials: credentials);
            return SecurityConfig.JwtTokenHandler.WriteToken(token); // Even more lazy here
        }
    }
}
