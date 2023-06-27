using EventManagement.AdminMVC.Common;
using EventManagement.Application.Contracts;
using EventManagement.Application.Event.Commands.MarkAsActive;
using EventManagement.Application.Event.Commands.MarkAsArchived;
using EventManagement.Application.Event.Queries.GetSubmittedEventsList;
using EventManagement.Application.Events.Queries.GetActiveEventsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.AdminMVC.Controllers
{
    [Route("/adminevent")]
    [Authorize(Roles = "Moderator, Admin")]
    public class AdminEventController : MVCControllerBase
    {
        private readonly IPhotoService _photoService;
        public AdminEventController(IMediator mediator, IPhotoService photoService) : base(mediator)
        {
            _photoService = photoService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var events = await Mediator.Send(new GetSubmittedEventsListQuery()).ConfigureAwait(false);
            foreach (var ev in events.Data)
            {
                ev.PhotoPath = _photoService.GetImageUrl(ev.PhotoPath);
            }

            return View(events.Data);
        }
        [HttpGet("markeventasactive/{id}")]
        public async Task<IActionResult> MarkEventAsActive(string id)
        {
            var model = new MarkEventAsActiveCommandModel { EventId = id };
            var result = await Mediator.Send(new MarkEventAsActiveCommand { Model = model }).ConfigureAwait(false);
            if (result.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return Redirect("/adminevent/index");
            }
            ModelState.AddModelError("", "Error marking event as active");

            return Redirect("/adminevent/index");
        }
        [HttpGet("markeventasarchived/{id}")]
        public async Task<IActionResult> MarkEventAsArchived(string id)
        {
            var model = new MarkEventAsArchivedCommandModel { EventId = id };
            var result = await Mediator.Send(new MarkEventAsArchivedCommand { Model = model }).ConfigureAwait(false);

            if (result.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return Redirect("/adminevent/index");
            }
            ModelState.AddModelError("", "Error marking event as archived");

            return Redirect("/adminevent/index");
        }
    }
}
