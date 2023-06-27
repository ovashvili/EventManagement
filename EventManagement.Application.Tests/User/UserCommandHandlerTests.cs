using EventManagement.Application.Commmon.Enums;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using EventManagement.Application.Users.Commands.AuthenticateUser;
using EventManagement.Application.Users.Commands.RegisterUser;
using EventManagement.Application.Users.Queries.GetAllUsers;
using FluentAssertions;
using Moq;

namespace EventManagement.Application.Tests.User
{
    public class UserCommandHandlerTests
    {
        [Fact]
        public async Task HandleAuthenticate_ValidCommand_ReturnsExpectedResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var data = new AuthenticateUserResponse
            {
                Id = "1",
                Name = "Test User",
                Username = "testuser",
                Email = "testuser@example.com",
                Token = "SampleToken"
            };
            var expectedResponse = new CommandResponse<AuthenticateUserResponse>(StatusCode.Success, null, data);

            var requestModel = new AuthenticateUserCommandModel
            {
                Email = "testuser@example.com",
                Password = "testpassword"
            };

            mockUserService.Setup(us => us.AuthenticateAsync(requestModel))
                .ReturnsAsync(expectedResponse);

            var handler = new AuthenticateUserCommandHandler(mockUserService.Object);
            var command = new AuthenticateUserCommand { Model = requestModel };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(expectedResponse.StatusCode);
            result.Message.Should().Be(expectedResponse.Message);
            result.Data.Should().BeEquivalentTo(expectedResponse.Data);

            mockUserService.Verify(us => us.AuthenticateAsync(requestModel), Times.Once());

        }
        [Fact]
        public async Task HandleAuthenticate_InvalidCommand_ReturnsErrorResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var expectedResponse = new CommandResponse<AuthenticateUserResponse>(Commmon.Enums.StatusCode.BadRequest, "Username or password is incorrect", null);

            var requestModel = new AuthenticateUserCommandModel
            {
                Email = "invaliduser",
                Password = "invalidpassword"
            };

            mockUserService.Setup(us => us.AuthenticateAsync(requestModel))
                .ReturnsAsync(expectedResponse);

            var handler = new AuthenticateUserCommandHandler(mockUserService.Object);
            var command = new AuthenticateUserCommand { Model = requestModel };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
            mockUserService.Verify(us => us.AuthenticateAsync(requestModel), Times.Once());
        }
        [Fact]
        public async Task HandleRegister_ValidCommand_ReturnsExpectedResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var expectedUserId = "1";
            var expectedResponse = new CommandResponse<string>(Commmon.Enums.StatusCode.Success, null, expectedUserId);

            var requestModel = new RegisterUserCommandModel
            {
                Name = "Test User",
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "testpassword"
            };

            mockUserService.Setup(us => us.RegisterAsync(requestModel, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var handler = new RegisterUserCommandHandler(mockUserService.Object);
            var command = new RegisterUserCommand { Model = requestModel };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
            mockUserService.Verify(us => us.RegisterAsync(requestModel, It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task HandleRegister_InvalidCommand_ReturnsErrorResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var expectedResponse = new CommandResponse<string>(Commmon.Enums.StatusCode.BadRequest, "Argument can't be null", null);

            var requestModel = new RegisterUserCommandModel
            {
                Name = "",
                Username = "",
                Email = "invalidemail",
                Password = "invalidpassword"
            };

            mockUserService.Setup(us => us.RegisterAsync(requestModel, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var handler = new RegisterUserCommandHandler(mockUserService.Object);
            var command = new RegisterUserCommand { Model = requestModel };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse, result);
            mockUserService.Verify(us => us.RegisterAsync(requestModel, It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}

