using EventManagement.Application.Commmon.Models;
using EventManagement.Infrastructure.Helpers.JWT;
using FluentFTP;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EventManagement.Infrastructure.Helpers
{
    public class ServiceHelper
    {
        public static string GenerateSecurityToken(string userName, string userId, List<string> userRoles, IOptions<JWTConfiguration> options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(options.Value.Secret);

            var claims = new List<Claim>
            {
                new Claim("UserId", userId),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpirationInMinutes),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static async Task<string> UploadFileAsync(string directoryPath, IFormFile photo)
        {
            var fileName = $"{Guid.NewGuid().ToString("N")}-{Path.GetFileName(photo.FileName)}";
            var filePath = Path.Combine(directoryPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream).ConfigureAwait(false);
            }

            return Path.Combine("images", fileName);
        }
    }
}
