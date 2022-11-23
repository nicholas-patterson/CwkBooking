using System;
using CwkBooking.Api.Services;
using CwkBooking.Api.Services.Abstractions;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwkBooking.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly MyFirstService _firstService;
        private readonly ISingletonOperation _singleton;
        private readonly ITransientOperation _transient;
        private readonly IScopedOperation _scoped;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(MyFirstService firstService, ISingletonOperation singleton, ITransientOperation transient, IScopedOperation scoped, ILogger<HotelsController> logger)
        {
            _firstService = firstService;
            _singleton = singleton;
            _transient = transient;
            _scoped = scoped;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Hotel>> GetAllHotels()
        {
            _logger.LogInformation($"GUID of singleton: {_singleton.Guid}");
            _logger.LogInformation($"GUID of transient: {_transient.Guid}");
            _logger.LogInformation($"GUID of scoped: {_scoped.Guid}");
            var hotels = _firstService.GetHotels();
            return hotels;
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotelById(int id)
        {
            var hotels = _firstService.GetHotels();
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
            var hotels = _firstService.GetHotels();
            hotels.Add(hotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateHotel(int id, [FromBody] Hotel hotel)
        {
            var hotels = _firstService.GetHotels();
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
            var hotels = _firstService.GetHotels();
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

