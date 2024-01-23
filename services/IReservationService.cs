using resevation_be.DTOs;

namespace resevation_be.Services
{
    public interface IReservationService
    {
        public Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string idToken);
        public Task<bool> ReseveAsync(ReservationDto reservation, string authentication);
    }
}