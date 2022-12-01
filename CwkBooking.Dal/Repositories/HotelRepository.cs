using CwkBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using CwkBooking.Domain.Abstractions.Repositories;

namespace CwkBooking.Dal.Repositories
{
    public class HotelRepository : IHotelsRepository
    {

        private readonly DataContext _ctx;

        public HotelRepository(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            _ctx.Hotels.Add(hotel);
            await _ctx.SaveChangesAsync();
            return hotel;

        }

        public async Task<Room> CreateHotelRoomAsync(int hotelId, Room room)
        {
            var hotel = await _ctx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(hotel => hotel.HotelId == hotelId);
            hotel.Rooms.Add(room);
            await _ctx.SaveChangesAsync();

            return room;
        }

        public async Task<Hotel> DeleteHotelAsync(int id)
        {
            var hotelToDelete = await _ctx.Hotels.FirstOrDefaultAsync((hotel) => hotel.HotelId == id);

            if (hotelToDelete is null)
            {
                return null;
            }

            _ctx.Hotels.Remove(hotelToDelete);
            await _ctx.SaveChangesAsync();
            return hotelToDelete;
        }

        public async Task<Room> DeleteRoomAsync(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(room => room.RoomId == roomId && room.HotelId == hotelId);

            if (room is null)
            {
                return null;
            }


            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();
            return room;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _ctx.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            var hotel = await _ctx.Hotels.Include(hotel => hotel.Rooms).FirstOrDefaultAsync((hotel) => hotel.HotelId == id);

            if (hotel is null)
            {
                return null;
            }

            return hotel;
        }

        public async Task<Room> GetRoomByIdAsync(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(room => room.HotelId == hotelId && room.RoomId == roomId);

            if (room is null)
            {
                return null;
            }
            return room;
        }

        public async Task<List<Room>> ListHotelRoomAsync(int hotelId)
        {
            return await _ctx.Rooms.Where(room => room.HotelId == hotelId).ToListAsync();
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel updatedHotel)
        {
            _ctx.Hotels.Update(updatedHotel);
            await _ctx.SaveChangesAsync();
            return updatedHotel;
        }

        public async Task<Room> UpdateRoomAsync(int hotelId, Room updatedRoom)
        {
            _ctx.Rooms.Update(updatedRoom);
            await _ctx.SaveChangesAsync();
            return updatedRoom;
        }
    }
}