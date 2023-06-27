using AutoMapper;
using EventManagement.Application.Contracts;
using EventManagement.Application.Events.Commands.CreateEvent;
using EventManagement.Application.Events.Commands.PurchaseTicket;
using EventManagement.Application.Events.Commands.ReserveTicket;
using EventManagement.Application.Events.Commands.UpdateEvent;
using EventManagement.Application.Events.Queries.GetActiveEventsList;
using EventManagement.Application.Events.Queries.GetEventDetails;
using EventManagement.MVC.Common;
using EventManagement.MVC.Infrastructure.Pager;
using EventManagement.MVC.Models;
using IdentityModel.OidcClient;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.MVC.Controllers
{
    [Route("/event")]
    public class EventController : MVCControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        public EventController(IMediator mediator, IPhotoService photoService, IMapper mapper) : base(mediator)
        {

            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index(int pg = 1, int pageSize = 3)
        {
            var events = await Mediator.Send(new GetActiveEventsListQuery()).ConfigureAwait(false);
            foreach (var ev in events.Data)
            {
                ev.PhotoPath = _photoService.GetImageUrl(ev.PhotoPath);
            }
            if (pg < 1)
                pg = 1;

            var totalEvents = events.Data.Count();

            var pager = new Pager(totalEvents, pg, pageSize);

            var eventsSkip = (pg - 1) * pageSize;

            var data = events.Data.Skip(eventsSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            var mappedEvents = data.Adapt<IEnumerable<EventViewModel>>();

            return View(mappedEvents);

        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            if (HttpContext.User.Identity is not null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return View();
                }
            }
            return Redirect("/identity/account/login");
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedModel = model.Adapt<CreateEventCommandModel>();
                mappedModel.Photo = model.Photo;
                var result = await Mediator.Send(new CreateEventCommand { Model = mappedModel }).ConfigureAwait(false);
                if (result.StatusCode == Application.Commmon.Enums.StatusCode.Success)
                {
                    return Redirect("/event/index");
                }
            }
            return View(model);

        }
        [HttpGet("details/{id}")]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        public async Task<IActionResult> Details(string id)
        {
            var eventDetails = await Mediator.Send(new GetEventByIdQuery { Id = id }).ConfigureAwait(false);

            eventDetails.Data.PhotoPath = _photoService.GetImageUrl(eventDetails.Data.PhotoPath);

            var mappedModel = eventDetails.Data.Adapt<EventViewModel>();

            return View(mappedModel);
        }
        [HttpGet("edit/{id}")]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (ModelState.IsValid)
            {
                var eventDetails = await Mediator.Send(new GetEventByIdQuery { Id = id }).ConfigureAwait(false);
                var res = eventDetails.Data.Adapt<UpdateEventViewModel>();
                return View(res);
            }
            ModelState.AddModelError("", "Error editing event");
            return View();

        }
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        public async Task<IActionResult> Edit(string id, UpdateEventViewModel model)
        {
            var mappedModel = model.Adapt<UpdateEventCommandModel>();
            mappedModel.Photo = model.Photo;

            if (ModelState.IsValid)
            {
                var response = await Mediator.Send(new UpdateEventCommand { Id = id, Model = mappedModel }).ConfigureAwait(false); ;

                if (response.StatusCode == Application.Commmon.Enums.StatusCode.Success)
                {
                    return RedirectToAction("Details", new { id = response.Data.Id });
                }
            }
            ModelState.AddModelError("", "Model state is not valid");
            return View(model);

        }
        [HttpGet("purchaseticket/{id}")]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        public async Task<IActionResult> PurchaseTicket(string id)
        {
            var model = new PurchaseTicketCommandModel { EventId = id };
            var result = await Mediator.Send(new PurchaseTicketCommand { Model = model }).ConfigureAwait(false);

            if (result.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet("reserveticket/{id}")]
        [Authorize(Roles = "Basic, Moderator, Admin")]
        public async Task<IActionResult> ReserveTicket(string id)
        {
            var model = new ReserveTicketCommandModel { EventId = id };
            var eventDetails = await Mediator.Send(new ReserveTicketCommand { Model = model }).ConfigureAwait(false);

            if (eventDetails.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", eventDetails.Message);

            return RedirectToAction("Details", new { id });
        }
    }
}
