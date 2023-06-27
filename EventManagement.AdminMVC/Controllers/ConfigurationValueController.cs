using EventManagement.AdminMVC.Models;
using EventManagement.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.AdminMVC.Controllers
{
    [Route("/configurationvalue")]
    [Authorize(Roles = "Admin")]
    public class ConfigurationValueController : Controller
    {
        private readonly IConfigurationValueService _configurationValueService;

        public ConfigurationValueController(IConfigurationValueService configurationValueService) => _configurationValueService = configurationValueService;

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var values = await _configurationValueService.GetAllValues().ConfigureAwait(false);
            if (values is not null)
            {
                return View(values);
            }
            return View(values);
        }
        [HttpGet("edit/{key}")]
        public async Task<IActionResult> Edit(string key)
        {
            if (string.Equals(key, "ReservationTime", StringComparison.OrdinalIgnoreCase))
            {
                var value = await _configurationValueService.GetReservationTimeAsync().ConfigureAwait(false);
                var data = new EditConfigurationValueModel { Key = key, Value = value };
                return View(data);
            }
            if (string.Equals(key, "EventEditDuration", StringComparison.OrdinalIgnoreCase))
            {
                var value = await _configurationValueService.GetEventEditDurationAsync().ConfigureAwait(false);
                var data = new EditConfigurationValueModel { Key = key, Value = value };
                return View(data);
            }
            return RedirectToAction("Index");

        }
        [HttpPost("edit/{key}")]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Edit(string key, EditConfigurationValueModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.Equals(model.Key, "ReservationTime", StringComparison.OrdinalIgnoreCase))
                {
                    await _configurationValueService.SetReservationTimeAsync(model.Value).ConfigureAwait(false);
                    return RedirectToAction("index");
                }
                if (string.Equals(model.Key, "EventEditDuration", StringComparison.OrdinalIgnoreCase))
                {
                    await _configurationValueService.SetEventEditDurationAsync(model.Value).ConfigureAwait(false);
                    return RedirectToAction("index");
                }
            }
            ModelState.AddModelError("", "Error editing event");
            return View(model);

        }
    }

}
