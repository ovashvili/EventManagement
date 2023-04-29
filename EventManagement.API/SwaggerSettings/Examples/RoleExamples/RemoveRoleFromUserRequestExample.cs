using EventManagement.Application.Role.Commands.RemoveRoleFromUser;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.RoleExamples
{
    public class RemoveRoleFromUserRequestExample : IExamplesProvider<RemoveRoleFromUserQuery>
    {
        public RemoveRoleFromUserQuery GetExamples()
        {
            return new RemoveRoleFromUserQuery { RoleName = "Admin", UserId = "cb975a17-fded-45fc-9ffe-4994d35520a1" };
        }
    }
}
