using AutoMapper;
using EventManagement.Application.Commmon.Enums;
using EventManagement.Application.Commmon.Models;
using EventManagement.Application.Contracts;
using EventManagement.Application.Events.Commands.CreateEvent;
using EventManagement.Application.Events.Commands.UpdateEvent;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Helpers;
using EventManagement.Infrastructure.Helpers.Utilities;
using EventManagement.Persistence.Context;
using EventManagement.Shared.Localizations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventManagement.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly EventDbContext _context;
        private readonly IUtility _utility;
        private readonly IMapper _mapper;
        private readonly IConfigurationValueService _configurationValueService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DirectoriesPathOptions _directoriesPathOptions;


        public EventService(EventDbContext context,
            IUtility utility,
            IMapper mapper,
            IConfigurationValueService cachedDataRepository,
            IOptions<DirectoriesPathOptions> directoriesPathOptions,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _context = context;
            _utility = utility;
            _mapper = mapper;
            _configurationValueService = cachedDataRepository;
            _directoriesPathOptions = directoriesPathOptions.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CommandResponse<EventDto>> CreateAsync(CreateEventCommandModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.ArgumentNull);
            }
            if(model.StartDate >= model.EndDate)
            {
                return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.DatesException);
            }
            var eventEntity = _mapper.Map<EventManagement.Domain.Entities.Event>(model);

            if (model.Photo != null)
            {
                if (string.IsNullOrEmpty(_directoriesPathOptions.EventFilesPath))
                {
                    return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.PhotoPathException);
                }
                if (string.IsNullOrEmpty(model.Photo.FileName))
                {
                    return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.InvalidFileName);
                }

                eventEntity.PhotoPath = await ServiceHelper.UploadFileAsync(_directoriesPathOptions.EventFilesPath, model.Photo).ConfigureAwait(false);
            }

            var userId = _utility.GetUserIdFromJWTToken();

            var modifiableTill = await _configurationValueService.GetEventEditDurationAsync(cancellationToken).ConfigureAwait(false);

            eventEntity.UserID = userId;

            eventEntity.IsActive = false;
            eventEntity.IsArchived = false;

            eventEntity.ModifiableTill = DateTime.Now.AddDays(Convert.ToInt32(modifiableTill));

            eventEntity.AvailableTickets = model.TicketQuantity;

            eventEntity.Id = Guid.NewGuid().ToString();
            var newTask = await _context.Events.AddAsync(eventEntity, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var eventDto = _mapper.Map<EventDto>(newTask.Entity);
            return new CommandResponse<EventDto>(StatusCode.Success, null, eventDto);
        }

        public async Task<CommandResponse<IEnumerable<EventDto>>> GetActiveEventListAsync(CancellationToken cancellationToken)
        {
            var activeEvents = await _context.Events
                    .Where(e => e.StartDate >= DateTime.Now && e.AvailableTickets > 0 && e.IsActive == true && e.IsArchived == false)
                    .ToListAsync(cancellationToken).ConfigureAwait(false);

            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(activeEvents);

            return new CommandResponse<IEnumerable<EventDto>>(StatusCode.Success, null, eventDtos);
        }
        public async Task<CommandResponse<IEnumerable<EventDto>>> GetAllEventListAsync(CancellationToken cancellationToken)
        {
            var activeEvents = await _context.Events.ToListAsync(cancellationToken).ConfigureAwait(false);

            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(activeEvents);

            return new CommandResponse<IEnumerable<EventDto>>(StatusCode.Success, null, eventDtos);
        }
        public async Task<CommandResponse<IEnumerable<EventDto>>> GetArchivedEventListAsync(CancellationToken cancellationToken)
        {
            var activeEvents = await _context.Events
                .Where(e => e.StartDate < DateTime.UtcNow || e.AvailableTickets == 0 || e.IsActive == false && e.IsArchived == true)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(activeEvents);

            return new CommandResponse<IEnumerable<EventDto>>(StatusCode.Success, null, eventDtos);
        }
        public async Task<CommandResponse<IEnumerable<EventDto>>> GetSubmittedEventsAsync(CancellationToken cancellationToken)
        {
            var activeEvents = await _context.Events
                    .Where(e => e.StartDate >= DateTime.Now && e.AvailableTickets > 0 && e.IsActive == false && e.IsArchived == false)
                    .ToListAsync(cancellationToken).ConfigureAwait(false);

            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(activeEvents);

            return new CommandResponse<IEnumerable<EventDto>>(StatusCode.Success, null, eventDtos);
        }

        public async Task<CommandResponse<EventDto>> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events.FindAsync(id).ConfigureAwait(false);
            if (eventEntity == null)
            {
                return new CommandResponse<EventDto>(StatusCode.NotFound, ErrorMessages.EventNotFound);
            }
            var reservations = await _context.TicketReservations
                .Where(tr => tr.EventId == id && tr.IsReserved == true)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            var eventDto = _mapper.Map<EventDto>(eventEntity);
            eventDto.AvailableTickets -= reservations.Count;
            return new CommandResponse<EventDto>(StatusCode.Success, null, eventDto);
        }

        public async Task<CommandResponse<string>> PurchaseTicketAsync(string id, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events.FindAsync(id).ConfigureAwait(false);
            if (eventEntity == null || eventEntity.IsActive == false || eventEntity.IsArchived == true)
            {
                return new CommandResponse<string>(StatusCode.NotFound, ErrorMessages.EventNotFound);
            }
            var userId = _utility.GetUserIdFromJWTToken();
            var reservations = await _context.TicketReservations
                .Where(tr => tr.EventId == id && tr.IsReserved == true)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            if (eventEntity.AvailableTickets <= 0 || eventEntity.AvailableTickets - reservations.Count <= 0)
            {
                return new CommandResponse<string>(StatusCode.BadRequest, ErrorMessages.NoAvailableTickets);
            }
            var hasReservation = reservations.FirstOrDefault(tr => tr.UserId == userId && tr.IsBought == false);
            if (hasReservation != null)
            {
                eventEntity.AvailableTickets--;
                hasReservation.IsBought = true;
                hasReservation.IsReserved = false;
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return new CommandResponse<string>(StatusCode.Success, "Ticket purchased successfully");
            }
            eventEntity.AvailableTickets--;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success, "Ticket purchased successfully");
        }

        public async Task<CommandResponse<string>> ReserveTicketAsync(string id, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events.FindAsync(id).ConfigureAwait(false);

            if (eventEntity == null || eventEntity.IsActive == false || eventEntity.IsArchived == true)
            {
                return new CommandResponse<string>(StatusCode.NotFound, ErrorMessages.EventNotFound);
            }

            var reservations = await _context.TicketReservations.Where(tr => tr.EventId == id && tr.IsReserved == true).ToListAsync(cancellationToken).ConfigureAwait(false);
            var userId = _utility.GetUserIdFromJWTToken();
            var hasReservation = reservations.FirstOrDefault(tr => tr.UserId == userId && tr.IsBought == false);
            if(hasReservation != null)
            {
                return new CommandResponse<string>(StatusCode.Conflict, ErrorMessages.TicketAlreadyReserved);

            }

            var reservationDuration = await _configurationValueService.GetReservationTimeAsync(cancellationToken).ConfigureAwait(false);

            var newReservation = new TicketReservations
            {
                Id = Guid.NewGuid().ToString(),
                EventId = eventEntity.Id,
                UserId = userId,
                IsReserved = true,
                ReservationTime = DateTime.UtcNow.AddMinutes(reservationDuration)
            };
            eventEntity.AvailableTickets--;
            await _context.TicketReservations.AddAsync(newReservation, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success, "Ticket reserved successfully");
        }

        public async Task<CommandResponse<EventDto>> UpdateAsync(string id, UpdateEventCommandModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.ArgumentNull);
            }
            if (model.StartDate >= model.EndDate)
            {
                return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.DatesException);
            }
            var eventEntity = await _context.Events.FindAsync(id).ConfigureAwait(false);
            if (eventEntity == null || eventEntity.IsActive == false || eventEntity.IsArchived == true)
            {
                return new CommandResponse<EventDto>(StatusCode.NotFound, ErrorMessages.EventNotFound);
            }
            if (eventEntity.ModifiableTill < DateTime.UtcNow)
            {
                return new CommandResponse<EventDto>(StatusCode.Unauthorized, ErrorMessages.EventNotEditable);

            }
            var userId = _utility.GetUserIdFromJWTToken();

            if(eventEntity.UserID != userId)
            {
                return new CommandResponse<EventDto>(StatusCode.Unauthorized, ErrorMessages.EventEditConflict);

            }
            if (model.TicketQuantity > eventEntity.TicketQuantity)
            {
                eventEntity.AvailableTickets += model.TicketQuantity - eventEntity.TicketQuantity;
            }

            _mapper.Map(model, eventEntity);

            if (model.Photo != null)
            {
                if (string.IsNullOrEmpty(_directoriesPathOptions.EventFilesPath))
                {
                    return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.PhotoPathException);
                }
                if (string.IsNullOrEmpty(model.Photo.FileName))
                {
                    return new CommandResponse<EventDto>(StatusCode.BadRequest, ErrorMessages.InvalidFileName);
                }

                eventEntity.PhotoPath = await ServiceHelper.UploadFileAsync(_directoriesPathOptions.EventFilesPath, model.Photo).ConfigureAwait(false);
            }
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var eventDto = _mapper.Map<EventDto>(eventEntity);
            return new CommandResponse<EventDto>(StatusCode.Success, null, eventDto);
        }
        public async Task<CommandResponse<string>> MarkAsActiveAsync(string id, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events.FindAsync(id).ConfigureAwait(false);

            if (eventEntity == null)
            {
                return new CommandResponse<string>(StatusCode.NotFound, ErrorMessages.EventNotFound);
            }
            if(eventEntity.IsActive == true)
            {
                return new CommandResponse<string>(StatusCode.Conflict, ErrorMessages.EventAlreadyMarkedAsActive);
            }
            eventEntity.IsActive = true;
            eventEntity.IsArchived = false;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success, "Event was marked as Active");
        }
        public async Task<CommandResponse<string>> MarkAsArchivedAsync(string id, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events.FindAsync(id).ConfigureAwait(false);

            if (eventEntity == null)
            {
                return new CommandResponse<string>(StatusCode.NotFound, ErrorMessages.EventNotFound);
            }
            if (eventEntity.IsArchived == true)
            {
                return new CommandResponse<string>(StatusCode.Conflict, ErrorMessages.EventAlreadyMarkedAsArchived);
            }
            eventEntity.IsActive = false;
            eventEntity.IsArchived = true;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new CommandResponse<string>(StatusCode.Success, "Event was marked as archived");
        }
    }
}
