using EventManagement.Application.Users.Commands.RegisterUser;
using Swashbuckle.AspNetCore.Filters;

namespace EventManagement.API.SwaggerSettings.Examples.UserExamples
{
    public class RegisterUserCommandModelExample : IExamplesProvider<RegisterUserCommandModel>
    {
        public RegisterUserCommandModel GetExamples()
        {
            return new RegisterUserCommandModel
            {
                Name = "John",
                Username = "johndoe",
                Email = "johndoe@gmail.com",
                Password = "JohnDoePassword!@"
            };
        }
    }
}
