using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using EventManagement.Application.Users.Queries.GetAllUsers;
using EventManagement.Application.Users.Queries.GetUserById;
using Moq;

namespace EventManagement.Application.Tests.User
{
    public class UserQueryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GetAllUsersQuery_ReturnsExpectedResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var users = new List<UserDto>
            {
                new UserDto { Id = "1", Name = "User 1", UserName = "user1", Email = "user1@example.com" },
                new UserDto { Id = "2", Name = "User 2", UserName = "user2", Email = "user2@example.com" },
            };
            var expectedResponse = new CommandResponse<IEnumerable<UserDto>>(Commmon.Enums.StatusCode.Success, null, users);

            mockUserService.Setup(us => us.GetAllUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var handler = new GetAllUsersQueryHandler(mockUserService.Object);
            var query = new GetAllUsersQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.Equal(expectedResponse, result);
            mockUserService.Verify(us => us.GetAllUsersAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task Handle_ValidId_ReturnsExpectedResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var user = new UserDto { Id = "1", Name = "User 1", UserName = "user1", Email = "user1@example.com" };
            var expectedResponse = new CommandResponse<UserDto>(Commmon.Enums.StatusCode.Success, null, user);

            mockUserService.Setup(us => us.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var handler = new GetUserByIdQueryHandler(mockUserService.Object);
            var query = new GetUserByIdQuery { Id = "1" };

            // Act
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.Equal(expectedResponse, result);
            mockUserService.Verify(us => us.GetByIdAsync("1", It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsErrorResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var expectedResponse = new CommandResponse<UserDto>(Commmon.Enums.StatusCode.NotFound, "User could not be found.", null);

            mockUserService.Setup(us => us.GetByIdAsync("invalid", It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var handler = new GetUserByIdQueryHandler(mockUserService.Object);
            var query = new GetUserByIdQuery { Id = "invalid" };

            // Act
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.Equal(expectedResponse, result);
            mockUserService.Verify(us => us.GetByIdAsync("invalid", It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
