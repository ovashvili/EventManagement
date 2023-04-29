using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Role.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserQueryValidator : AbstractValidator<RemoveRoleFromUserQuery>
    {
        public RemoveRoleFromUserQueryValidator()
        {
            _ = RuleFor(x => x.UserId)
                .NotEmpty();

            _ = RuleFor(x => x.RoleName)
                .NotEmpty();
        }
    }
}
