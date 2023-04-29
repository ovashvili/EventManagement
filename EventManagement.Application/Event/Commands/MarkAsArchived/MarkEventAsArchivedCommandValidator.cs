using FluentValidation;

namespace EventManagement.Application.Event.Commands.MarkAsArchived
{
    public class MarkEventAsArchivedCommandValidator : AbstractValidator<MarkEventAsArchivedCommand>
    {
        public MarkEventAsArchivedCommandValidator()
        {
            RuleFor(x => x.Model.EventId)
                .NotEmpty();
        }
    }
}
