using EventManagement.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers.v1
{
    [Route("/AdminActions")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class ConfigurationValueController : ControllerBase
    {
        private readonly IConfigurationValueService _configurationValueService;
        public ConfigurationValueController(IConfigurationValueService configurationValueService)
        {
            _configurationValueService = configurationValueService;
        }

        [HttpPost("set-reservation-time")]
        public async Task<ActionResult> SetReservationTime([FromBody] int reservationTime, CancellationToken cancellationToken)
        {
            await _configurationValueService.SetReservationTimeAsync(reservationTime, cancellationToken).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("get-reservation-time")]
        public async Task<IActionResult> GetReservationTime(CancellationToken cancellationToken)
        {
            var reservationTime = await _configurationValueService.GetReservationTimeAsync(cancellationToken).ConfigureAwait(false);
            return Ok(reservationTime);
        }

        [HttpPost("set-event-edit-duration")]
        public async Task<IActionResult> SetEventEditDuration([FromBody] int editDuration, CancellationToken cancellationToken)
        {
            await _configurationValueService.SetEventEditDurationAsync(editDuration, cancellationToken).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("get-event-edit-duration")]
        public async Task<IActionResult> GetEventEditDuration(CancellationToken cancellationToken)
        {
            var editDuration = await _configurationValueService.GetEventEditDurationAsync(cancellationToken).ConfigureAwait(false);
            return Ok(editDuration);
        }
    }
}
