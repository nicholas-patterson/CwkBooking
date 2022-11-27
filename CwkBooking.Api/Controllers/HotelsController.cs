using AutoMapper;
using CwkBooking.Api.Dto;
using CwkBooking.Domain.Abstractions.Repositories;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwkBooking.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(ILogger<HotelsController> logger, IMapper mapper, IHotelsRepository hotelsRepository)
        {
            _logger = logger;
            _hotelsRepository = hotelsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelGetDto>>> GetAllHotels()
        {
            var hotels = await _hotelsRepository.GetAllHotelsAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);
            return hotelsGet;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelGetDto>> GetHotelById(int id)
        {
            var hotel = await _hotelsRepository.GetHotelByIdAsync(id);

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

            await _hotelsRepository.CreateHotelAsync(domainHotel);

            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotelGet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHotel(HotelCreateDto updatedHotel, int id)
        {
            var toUpdate = _mapper.Map<Hotel>(updatedHotel);
            toUpdate.HotelId = id;
            await _hotelsRepository.UpdateHotelAsync(toUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            var hotelToDelete = await _hotelsRepository.DeleteHotelAsync(id);

            if (hotelToDelete is null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomGetDto>>> GetAllHotelRooms(int hotelId)
        {
            var rooms = await _hotelsRepository.ListHotelRoomAsync(hotelId);
            var mappedRooms = _mapper.Map<List<RoomGetDto>>(rooms);
            return mappedRooms;
        }

        [HttpGet("{hotelId}/rooms/{roomId}")]
        public async Task<ActionResult<RoomGetDto>> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _hotelsRepository.GetRoomByIdAsync(hotelId, roomId);

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
            var room = _mapper.Map<Room>(newRoom);

            await _hotelsRepository.CreateHotelRoomAsync(hotelId, room);

            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotelId, roomId = mappedRoom.RoomId }, mappedRoom);
        }

        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<ActionResult> UpdateHotelRoom(RoomPostPutDto updatedRoom, int hotelId, int roomId)
        {
            var toUpdate = _mapper.Map<Room>(updatedRoom);
            toUpdate.RoomId = roomId;
            toUpdate.HotelId = hotelId;

            await _hotelsRepository.UpdateRoomAsync(hotelId, toUpdate);
            return NoContent();
        }


        [HttpDelete("{hotelId}/rooms/{roomId}")]
        public async Task<ActionResult> RemoveRoomFromHotel(int hotelId, int roomId)
        {
            var room = await _hotelsRepository.DeleteRoomAsync(hotelId, roomId);

            if (room is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

