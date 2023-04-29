using EventManagement.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhotoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetImageUrl(string relativeImagePath)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}/{relativeImagePath}";
        }
    }
}
