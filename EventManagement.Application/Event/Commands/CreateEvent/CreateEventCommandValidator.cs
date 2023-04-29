using FluentValidation;

namespace EventManagement.Application.Events.Commands.CreateEvent
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateTaskCommandValidator()
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
