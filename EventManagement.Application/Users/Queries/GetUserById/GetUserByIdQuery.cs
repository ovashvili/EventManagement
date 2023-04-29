using EventManagement.Application.Commmon.Models;
using MediatR;

namespace EventManagement.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<CommandResponse<UserDto>>
    {
        public string Id { get; set; }
    }
}
