using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EventManagement.Infrastructure.Middlewares.JwtTokenvalidator
{
    public class JwtTokenValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtTokenValidatorMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public JwtTokenValidatorMiddleware(RequestDelegate next, ILogger<JwtTokenValidatorMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var token = context.Request.Cookies["JwtToken"];

            if (!string.IsNullOrEmpty(token))
            {
                _logger.LogInformation("Token found in cookie: {Token}", token);

                var tokenValidationParameters = GetTokenValidationParameters(context);

                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                    if (principal != null && validatedToken != null && validatedToken.ValidTo >= DateTime.UtcNow)
                    {
                        _logger.LogInformation("Token validation successful.");

                        var identity = new ClaimsIdentity(principal.Claims, JwtBearerDefaults.AuthenticationScheme);
                        context.User = new ClaimsPrincipal(identity);

                        _logger.LogInformation("Setting HttpContext.User with authenticated identity.");
                    }
                    else
                    {
                        _logger.LogInformation("Token validation failed or token is expired.");
                        context.User = new ClaimsPrincipal(new ClaimsIdentity());
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error validating token.");
                }
            }
            else
            {
                _logger.LogInformation("No token found in cookie.");
            }

            await _next(context).ConfigureAwait(false);
        }

        private TokenValidationParameters GetTokenValidationParameters(Microsoft.AspNetCore.Http.HttpContext context)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "Issuer",
                ValidAudience = "Audience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfiguration:Secret"])),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                {
                    if (expires != null)
                    {
                        if (DateTime.UtcNow > expires)
                        {
                            _logger.LogWarning("Token has expired.");
                            return false;
                        }
                    }
                    return true;
                }
            };
        }
    }
}

