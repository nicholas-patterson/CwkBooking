using CwkBooking.Dal;
using CwkBooking.Domain.Abstractions.Repositories;
using CwkBooking.Domain.Abstractions.Services;
using CwkBooking.Domain.Models;

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
        public async Task<Reservation> MakeReservation(Reservation reservation)
        {
            // Step 1: Create a reservation instance
            // var reservation = new Reservation
            // {
            //     HotelId = hotelId,
            //     RoomId = roomId,
            //     CheckInDate = checkIn,
            //     CheckoutDate = checkOut,
            //     Customer = customer,
            // };

            // Step 2: Get the hotel, including all rooms
            var hotel = await _hotelRepository.GetHotelByIdAsync(reservation.HotelId);

            // Step 3: Find the specified room
            var room = hotel.Rooms.Where(room => room.RoomId == reservation.RoomId).FirstOrDefault();

            if (room == null)
            {
                return null;
            }

            // Step 4: Make sure room is available
            var roomBusyFrom = room.BusyFrom == null ? default(DateTime) : room.BusyFrom;
            var roomBusyTo = room.BusyTo == null ? default(DateTime) : room.BusyTo;
            var isBusy = reservation.CheckInDate >= roomBusyFrom && reservation.CheckInDate <= roomBusyTo;

            if (isBusy)
            {
                return null;
            }

            if (room.NeedsRepair)
            {
                return null;
            }
            // Step 5: Set busyfrom and busyto on the room
            room.BusyFrom = reservation.CheckInDate;
            room.BusyTo = reservation.CheckoutDate;

            // Step 6: Persist all changes to the database
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();

            return reservation;
        }
    }
}