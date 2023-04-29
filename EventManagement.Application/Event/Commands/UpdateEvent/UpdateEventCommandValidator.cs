using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Model.Name)
                .MaximumLength(40)
                 .NotEmpty();

            RuleFor(x => x.Model.Address)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Model.Description)
                .MaximumLength(60)
                .NotEmpty();

            RuleFor(x => x.Model.City)
                .MaximumLength(85)
                .NotEmpty();

            RuleFor(x => x.Model.StartDate)
                .GreaterThan(DateTime.UtcNow);

            RuleFor(x => x.Model.EndDate)
                .GreaterThan(DateTime.UtcNow);
        }

    }
}
