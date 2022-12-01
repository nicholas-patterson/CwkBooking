using CwkBooking.Dal;
using CwkBooking.Domain.Abstractions.Repositories;
using CwkBooking.Domain.Abstractions.Services;
using CwkBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CwkBooking.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository _hotelRepository;
        private readonly DataContext _ctx;

        public ReservationService(IHotelsRepository hotelRepository, DataContext ctx)
        {
            _hotelRepository = hotelRepository;
            _ctx = ctx;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _ctx.Reservations
                .Include(r => r.Room)
                .Include(r => r.Hotel)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            var reservation = await _ctx.Reservations
                .Include(r => r.Room)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return null;
            };

            return reservation;
        }

        public async Task<Reservation> MakeReservationAsync(Reservation reservation)
        {
            // Step 1: Get the hotel, including all rooms
            var hotel = await _hotelRepository.GetHotelByIdAsync(reservation.HotelId);

            // Step 2: Find the specified room
            var room = hotel.Rooms.Where(room => room.RoomId == reservation.RoomId).FirstOrDefault();

            if (hotel == null || room == null)
            {
                return null;
            }

            // Step 3: Make sure room is available
            bool isBusy = await _ctx.Reservations.AnyAsync(r =>
                (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckoutDate)
                && (reservation.CheckoutDate >= r.CheckInDate && reservation.CheckoutDate <= r.CheckoutDate)
            );

            if (isBusy || room.NeedsRepair)
            {
                return null;
            }

            // Step 4: Persist all changes to the database
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> CancelReservationAsync(int id)
        {
            var reservation = await _ctx.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return null;
            }

            _ctx.Reservations.Remove(reservation);
            await _ctx.SaveChangesAsync();
            return reservation;
        }
    }
}