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

        public HotelsController(ILogger<HotelsController> logger, DataContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            return hotels;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelById(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync((hotel) => hotel.HotelId == id);

            if (hotel is null)
            {
                return NotFound();
            }

            return hotel;
        }

        [HttpPost]
        public async Task<ActionResult> CreateHotel(Hotel hotel)
        {
            _ctx.Hotels.Add(hotel);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHotel(int id, Hotel hotel)
        {
            var hotelToUpdate = await _ctx.Hotels.FirstOrDefaultAsync((hotel) => hotel.HotelId == id);

            if (hotelToUpdate is null)
            {
                return NotFound();
            }

            hotelToUpdate.Stars = hotel.Stars;
            hotelToUpdate.Description = hotel.Description;
            hotelToUpdate.Name = hotel.Name;

            _ctx.Hotels.Update(hotel);
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

