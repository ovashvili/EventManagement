using EventManagement.Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;

namespace EventManagement.Infrastructure.Services
{
    public class ConfigurationValueService : IConfigurationValueService
    {
        private readonly IDistributedCache _cache;

        public ConfigurationValueService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetReservationTimeAsync(int reservationTime, CancellationToken cancellationToken = default)
        {
            await _cache.SetStringAsync("ReservationTime", reservationTime.ToString(), new DistributedCacheEntryOptions(), cancellationToken).ConfigureAwait(false);
        }

        public async Task<int> GetReservationTimeAsync(CancellationToken cancellationToken = default)
        {
            var reservationTime = await _cache.GetStringAsync("ReservationTime", cancellationToken).ConfigureAwait(false);
            return reservationTime != null ? int.Parse(reservationTime) : -1;
        }

        public async Task SetEventEditDurationAsync(int editDuration, CancellationToken cancellationToken = default)
        {
            await _cache.SetStringAsync("EventEditDuration", editDuration.ToString(), new DistributedCacheEntryOptions(), cancellationToken).ConfigureAwait(false);
        }

        public async Task<int> GetEventEditDurationAsync(CancellationToken cancellationToken = default)
        {
            var editDuration = await _cache.GetStringAsync("EventEditDuration", cancellationToken).ConfigureAwait(false);
            return editDuration != null ? int.Parse(editDuration) : -1;
        }
        public async Task<Dictionary<string, int>> GetAllValues(CancellationToken cancellationToken = default)
        {
            var reservationTime = await GetReservationTimeAsync(cancellationToken).ConfigureAwait(false);
            var editDuration = await GetEventEditDurationAsync(cancellationToken).ConfigureAwait(false);
            var values = new Dictionary<string, int>
            {
                { "ReservationTime", reservationTime  },
                { "EventEditDuration", editDuration}

            };
            return values;
        }
    }
}

