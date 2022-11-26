using AutoMapper;
using CwkBooking.Api.Dto;
using CwkBooking.Dal;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CwkBooking.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly DataContext _ctx;

        private readonly IMapper _mapper;

        public HotelsController(ILogger<HotelsController> logger, DataContext ctx, IMapper mapper)
        {
            _logger = logger;
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelGetDto>>> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);
            return hotelsGet;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelGetDto>> GetHotelById(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync((hotel) => hotel.HotelId == id);

            if (hotel is null)
            {
                return NotFound();
            }

            var hotelGet = _mapper.Map<HotelGetDto>(hotel);

            return hotelGet;
        }

        [HttpPost]
        public async Task<ActionResult<HotelGetDto>> CreateHotel(HotelCreateDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);

            _ctx.Hotels.Add(domainHotel);
            await _ctx.SaveChangesAsync();

            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotelGet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHotel(HotelCreateDto updatedHotel, int id)
        {
            var toUpdate = _mapper.Map<Hotel>(updatedHotel);
            toUpdate.HotelId = id;
            _ctx.Hotels.Update(toUpdate);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            var hotelToDelete = await _ctx.Hotels.FirstOrDefaultAsync((hotel) => hotel.HotelId == id);

            if (hotelToDelete is null)
            {
                return NotFound();
            }

            _ctx.Hotels.Remove(hotelToDelete);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomGetDto>>> GetAllHotelRooms(int hotelId)
        {
            var rooms = await _ctx.Rooms.Where(room => room.HotelId == hotelId).ToListAsync();
            var mappedRooms = _mapper.Map<List<RoomGetDto>>(rooms);
            return mappedRooms;
        }

        [HttpGet("{hotelId}/rooms/{roomId}")]
        public async Task<ActionResult<RoomGetDto>> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(room => room.HotelId == hotelId && room.RoomId == roomId);

            if (room is null)
            {
                return NotFound();
            }

            var mappedRoom = _mapper.Map<RoomGetDto>(room);
            return mappedRoom;

        }

        [HttpPost("{hotelId}/rooms")]
        public async Task<ActionResult<RoomGetDto>> AddHotelRoom(RoomPostPutDto newRoom, int hotelId)
        {
            var hotel = await _ctx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(hotel => hotel.HotelId == hotelId);

            if (hotel is null)
            {
                return NotFound();
            }
            var dtoToRoom = _mapper.Map<Room>(newRoom);
            hotel.Rooms.Add(dtoToRoom);

            var roomToRoomGet = _mapper.Map<RoomGetDto>(dtoToRoom);
            _logger.LogInformation($"&&&: {roomToRoomGet.RoomId}");
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotel.HotelId, roomId = roomToRoomGet.RoomId }, roomToRoomGet);
        }

        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<ActionResult> UpdateHotelRoom(RoomPostPutDto updatedRoom, int hotelId, int roomId)
        {
            var toUpdate = _mapper.Map<Room>(updatedRoom);
            toUpdate.RoomId = roomId;
            toUpdate.HotelId = hotelId;

            _ctx.Rooms.Update(toUpdate);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{hotelId}/rooms/{roomId}")]
        public async Task<ActionResult> RemoveRoomFromHotel(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(room => room.RoomId == roomId && room.HotelId == hotelId);

            if (room is null)
            {
                return NotFound();
            }


            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}

