using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CommandResponse<EventDto>>
    {
        private readonly IEventService _eventService;

        public CreateEventCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<CommandResponse<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            return await _eventService.CreateAsync(request.Model, cancellationToken).ConfigureAwait(false);
        }
    }
}
