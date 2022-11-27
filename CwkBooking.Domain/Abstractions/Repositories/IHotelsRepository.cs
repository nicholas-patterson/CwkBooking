using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CwkBooking.Domain.Models;

namespace CwkBooking.Domain.Abstractions.Repositories
{
    public interface IHotelsRepository
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> UpdateHotelAsync(Hotel updatedHotel);
        Task<Hotel> DeleteHotelAsync(int id);
        Task<List<Room>> ListHotelRoomAsync(int hotelId);
        Task<Room> GetRoomByIdAsync(int hotelId, int roomId);
        Task<Room> CreateHotelRoomAsync(int hotelId, Room room);
        Task<Room> UpdateRoomAsync(int hotelId, Room updatedRoom);
        Task<Room> DeleteRoomAsync(int hotelId, int roomId);
    }
}