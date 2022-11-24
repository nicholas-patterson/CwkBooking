using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwkBooking.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly HttpContext _http;

        public HotelsController(ILogger<HotelsController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _http = httpContextAccessor.HttpContext;
        }

        [HttpGet]
        public ActionResult<List<Hotel>> GetAllHotels()
        {
            _http.Request.Headers.TryGetValue("ny-middleware-header", out var headerDate);
            return Ok(headerDate);
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotelById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<Hotel> CreateHotel([FromBody] Hotel hotel)
        {

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateHotel(int id, [FromBody] Hotel hotel)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHotel(int id)
        {
            return NoContent();
        }
    }
}

