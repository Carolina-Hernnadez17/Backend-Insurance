using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Insurance.Infrastructure.Security;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(
        int userId,
        string usuario,
        string rol)
    {
        var key =
            _configuration["Jwt:Key"];

        var issuer =
            _configuration["Jwt:Issuer"];

        var audience =
            _configuration["Jwt:Audience"];

        var expireMinutes =
            int.Parse(
                _configuration["Jwt:ExpireMinutes"]!);

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()),

            new Claim(
                ClaimTypes.Name,
                usuario),

            new Claim(
                ClaimTypes.Role,
                rol)
        };

        var token =
            new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires:
                DateTime.Now.AddMinutes(
                    expireMinutes),
                signingCredentials:
                credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}