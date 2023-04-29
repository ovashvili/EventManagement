using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            _ = RuleFor(x => x.Model.Username)
                .NotEmpty().WithMessage("Your username cannot be empty")
                .MaximumLength(20).WithMessage("Your username length must not exceed 20.");

            _ = RuleFor(x => x.Model.Name)
                .NotEmpty().WithMessage("Your firstname cannot be empty")
                .MaximumLength(60).WithMessage("Your username length must not exceed 60.");

            _ = RuleFor(p => p.Model.Password)
                .NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[][""!@$%^&#*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Your password must contain one or more special characters.")
                .Matches("^[^ ]*$").WithMessage("Your password must not contain the spaces.");
        }
    }
}
