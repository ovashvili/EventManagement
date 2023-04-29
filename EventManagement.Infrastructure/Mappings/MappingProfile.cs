using AutoMapper;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Events.Commands.CreateEvent;
using EventManagement.Application.Events.Commands.UpdateEvent;
using EventManagement.Application.Users.Commands.AuthenticateUser;
using EventManagement.Application.Users.Commands.RegisterUser;
using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommandModel, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            
            CreateMap<CreateEventCommandModel, Event>();
            CreateMap<AuthenticateUserCommandModel, User>();
            CreateMap<User, AuthenticateUserCommandModel>();
            CreateMap<User, AuthenticateUserResponse>();
            CreateMap<AuthenticateUserResponse, User>();

            CreateMap<UpdateEventCommandModel, Event>();

            CreateMap<Event, EventDto>();

            CreateMap<EventDto, UpdateEventCommandModel>();

            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
