using EventManagement.Application.Role.Commands.RemoveRoleFromUser;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.RoleExamples
{
    public class RemoveRoleFromUserQueryExample : IExamplesProvider<RemoveRoleFromUserQuery>
    {
        public RemoveRoleFromUserQuery GetExamples()
        {
            return new RemoveRoleFromUserQuery
            {
                UserId = "cb975a17-fded-45fc-9ffe-4994d35520a1",
                RoleName = "Moderator"
            };
        }
    }
}
