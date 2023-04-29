using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(x => x.Model.Email)
                .NotEmpty()
                .Matches(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")
                .WithMessage("Email should not be empty and must be valid email format.");

            RuleFor(x => x.Model.Password)
                .NotEmpty();
        }
    }
}
