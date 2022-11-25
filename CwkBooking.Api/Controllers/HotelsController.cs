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
    }
}

