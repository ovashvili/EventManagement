using FluentValidation;

namespace EventManagement.Application.Events.Commands.ReserveTicket
{
    public class ReserveTicketCommandValidator : AbstractValidator<ReserveTicketCommand>
    {
        public ReserveTicketCommandValidator()
        {
            RuleFor(x => x.Model.EventId)
                .NotEmpty();
        }
    }
}
