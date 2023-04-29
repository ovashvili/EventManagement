using EventManagement.Application.Role.Commands.AddRoleToUser;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Examples;

namespace EventManagement.API.SwaggerSettings.Examples.RoleExamples
{
    public class AddRoleToUserCommandModelExample : IExamplesProvider<AddRoleToUserCommand>
    {
        public AddRoleToUserCommand GetExamples()
        {
            return new AddRoleToUserCommand
            {
                UserId = "cb975a17-fded-45fc-9ffe-4994d35520a1",
                RoleName = "Moderator"
            };
        }
    }
}
