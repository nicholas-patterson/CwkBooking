using System;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwkBooking.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly DataSource _dataSource;

        public HotelsController(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [HttpGet]
        public ActionResult<List<Hotel>> GetAllHotels()
        {
            var hotels = _dataSource.Hotels;
            return hotels;
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotelById(int id)
        {
            var hotels = _dataSource.Hotels;
            var hotel = hotels.FirstOrDefault(hotel => hotel.HotelId == id);

            if(hotel == null)
            {
                return NotFound();
            }
            return hotel;
        }

        [HttpPost]
        public ActionResult<Hotel> CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _dataSource.Hotels;
            hotels.Add(hotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateHotel(int id, [FromBody] Hotel hotel)
        {
            var hotels = _dataSource.Hotels;
            var hotelToBeUpdated = hotels.FirstOrDefault(hotel => hotel.HotelId == id);

            if(hotelToBeUpdated == null)
            {
                return NotFound();
            }

            hotels.Remove(hotelToBeUpdated);
            hotels.Add(hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHotel(int id)
        {
            var hotels = _dataSource.Hotels;
            var hotelToDelete = hotels.FirstOrDefault(hotel => hotel.HotelId == id);

            if (hotelToDelete == null)
            {
                return NotFound();
            }

            hotels.Remove(hotelToDelete);
            return NoContent();
        }
    }
}

