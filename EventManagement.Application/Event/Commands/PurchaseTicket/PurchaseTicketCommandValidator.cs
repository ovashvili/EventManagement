using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Commands.PurchaseTicket
{
    public class PurchaseTicketCommandValidator : AbstractValidator<PurchaseTicketCommand>
    {
        public PurchaseTicketCommandValidator()
        {
            RuleFor(x => x.Model.EventId)
                .NotEmpty();
        }
    }
}
