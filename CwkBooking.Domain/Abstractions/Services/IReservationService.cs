using CwkBooking.Domain.Models;

namespace CwkBooking.Domain.Abstractions.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservationAsync(Reservation reservation);
        Task<List<Reservation>> GetAllReservationsAsync();

        Task<Reservation> GetReservationByIdAsync(int id);

        Task<Reservation> CancelReservationAsync(int id);
    }
}