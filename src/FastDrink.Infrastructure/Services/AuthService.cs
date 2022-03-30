using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Settings;
using FastDrink.Domain.Entities;
using HashidsNet;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FastDrink.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IHashids _hashids;

    public AuthService(JwtSettings jwtSettings, IHashids hashids)
    {
        _jwtSettings = jwtSettings;
        _hashids = hashids;
    }

    public byte[] GenerateSalt()
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return salt;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,_hashids.Encode(user.Id)),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.Role != null ? user.Role.Name : "" )
            }),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Audience = "https://localhost:7011",
            Issuer = "https://localhost:7011",
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string HashPassword(string requestPassword, byte[] salt)
    {
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            requestPassword,
            salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hash;
    }
}
