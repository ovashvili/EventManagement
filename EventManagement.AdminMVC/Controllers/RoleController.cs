using EventManagement.AdminMVC.Common;
using EventManagement.Application.Role.Commands.AddRoleToUser;
using EventManagement.Application.Role.Commands.RemoveRoleFromUser;
using EventManagement.Application.Users.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.AdminMVC.Controllers
{
    [Route("/role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : MVCControllerBase
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var events = await Mediator.Send(new GetAllUsersQuery()).ConfigureAwait(false);
            if (events is not null)
            {
                return View(events.Data);
            }
            return View();
        }
        [HttpGet("removerolebasicfromuser/{id}/rolename/{roleNAme}")]
        public async Task<IActionResult> RemoveRoleBasicFromUser(string id, string roleName)
        {
            var res = await Mediator.Send(new RemoveRoleFromUserQuery { UserId = id, RoleName = roleName }).ConfigureAwait(false);
            if (res.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Error reserving ticket");
            return RedirectToAction("index");
        }
        [HttpGet("removerolemoderatorfromuser/{id}/rolename/{roleNAme}")]
        public async Task<IActionResult> RemoveRoleModeratorFromUser(string id, string roleName)
        {
            var res = await Mediator.Send(new RemoveRoleFromUserQuery { UserId = id, RoleName = roleName }).ConfigureAwait(false);
            if (res.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Error reserving ticket");
            return RedirectToAction("index");
        }
        [HttpGet("addrolebasictouser/{id}/rolename/{roleNAme}")]
        public async Task<IActionResult> AddRoleBasicToUser(string id, string roleName)
        {
            var res = await Mediator.Send(new AddRoleToUserCommand { UserId = id, RoleName = roleName }).ConfigureAwait(false);
            if (res.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Error reserving ticket");
            return RedirectToAction("index");
        }

        [HttpGet("addrolemoderatortouser/{id}/rolename/{roleNAme}")]
        public async Task<IActionResult> AddRoleModeratorToUser(string id, string roleName)
        {
            var res = await Mediator.Send(new AddRoleToUserCommand { UserId = id, RoleName = roleName });
            if (res.StatusCode == Application.Commmon.Enums.StatusCode.Success)
            {
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Error reserving ticket");
            return RedirectToAction("index");
        }

    }
}
