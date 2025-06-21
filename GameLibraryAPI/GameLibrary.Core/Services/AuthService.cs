using GameLibrary.Database.Entities;
using GameLibrary.Infrastructure.Config;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameLibrary.Core.Services
{
    public class AuthService
    {
        public string GetToken(User user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JWTSettings.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var idClaim = new Claim("userId", user.Id.ToString());
            var infoClaim = new Claim("email", user.Email);
            var roleClaim = new Claim("role", role);


            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim }),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public string HashPassword(string password, byte[] salt)
        {
            var bytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(bytes);
        }
    }
}