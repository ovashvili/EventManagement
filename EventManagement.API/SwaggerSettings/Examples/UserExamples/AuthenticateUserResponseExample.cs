using EventManagement.Application.Users.Commands.AuthenticateUser;
using Swashbuckle.AspNetCore.Filters;

namespace EventManagement.API.SwaggerSettings.Examples.UserExamples
{
    public class AuthenticateUserResponseExample : IExamplesProvider<AuthenticateUserResponse>
    {
        public AuthenticateUserResponse GetExamples()
        {
            return new AuthenticateUserResponse
            {
                Id = "1c9e4b27-1f4e-4d17-9332-e608e2328a96",
                Name = "John",
                Username = "johndoe",
                Email = "johndoe@gmail.com",
                Token = "eyJhbGciOiJIUz11Ni1s1nR5cC16 IkpXVCJ9. eyJzdW1iOi1xMj MONTY 30Dkw1iwibmFtZS161kpvaG4gRG9IliwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ"
            };
        }
    }
}
