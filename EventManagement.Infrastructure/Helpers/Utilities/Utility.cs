using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Helpers.Utilities
{
    public class Utility : IUtility
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Utility(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string? GetUserIdFromJWTToken()
        {
            var context = _contextAccessor.HttpContext;
            IEnumerable<Claim> claims;
            if (context is not null)
            {
                claims = context.User.Claims;
                if (claims is not null)
                {
                    var userId = claims.FirstOrDefault(c => c.Type == "UserId").ToString();
                    userId = userId.Substring(8, userId.Length - 8);

                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new InvalidDataException("User was not found");
                    }
                    return userId;
                }
            }
            return null;
        }
    }
}
