using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

namespace BookStoreManagementSystem.Helpers
{
    public static class JwtHelper
    {
        private static readonly string SecretKey =
            ConfigurationManager.AppSettings["JwtSecretKey"];

        private static readonly string Issuer =
            ConfigurationManager.AppSettings["JwtIssuer"];

        private static readonly string Audience =
            ConfigurationManager.AppSettings["JwtAudience"];

        private static readonly int ExpiryMinutes =
            int.Parse(ConfigurationManager.AppSettings["JwtExpiryMinutes"]);

        public static string GenerateToken(int userId, string email, bool isAdmin)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(SecretKey));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        // ✅ Validate JWT
        public static ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(key),

                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(
                token, parameters, out _);
        }
    }
}
