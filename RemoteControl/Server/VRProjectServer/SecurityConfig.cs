using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace VRProjectServer;

public static class SecurityConfig
{
    public static readonly SymmetricSecurityKey SecurityKey = new(Guid.NewGuid().ToByteArray());
    public static readonly JwtSecurityTokenHandler JwtTokenHandler = new();
}
