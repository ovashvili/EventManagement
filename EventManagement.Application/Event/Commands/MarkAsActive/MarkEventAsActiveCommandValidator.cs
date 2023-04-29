using FluentValidation;

namespace EventManagement.Application.Event.Commands.MarkAsActive
{
    public class MarkEventAsActiveCommandValidator : AbstractValidator<MarkEventAsActiveCommand>
    {
        public MarkEventAsActiveCommandValidator()
        {
            RuleFor(x => x.Model.EventId)
                .NotEmpty();
        }
    }
}
