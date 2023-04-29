using EventManagement.Application.Commmon.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.RoleExamples
{
    public class GetUserRolesQueryResponseExample : IExamplesProvider<IEnumerable<RoleDto>>
    {
        public IEnumerable<RoleDto> GetExamples()
        {
            return new List<RoleDto>
            {
                new RoleDto { Role = "Admin" },
                new RoleDto { Role = "Moderator"}
            };
        }
    }
}
