namespace EventManagement.Application.Contracts
{
    public interface IConfigurationValueService
    {
        Task SetReservationTimeAsync(int reservationTime, CancellationToken cancellationToken = default);
        Task<int> GetReservationTimeAsync(CancellationToken cancellationToken = default);
        Task SetEventEditDurationAsync(int editDuration, CancellationToken cancellationToken = default);
        Task<int> GetEventEditDurationAsync(CancellationToken cancellationToken = default);
        Task<Dictionary<string, int>> GetAllValues(CancellationToken cancellationToken = default);
    }
}
