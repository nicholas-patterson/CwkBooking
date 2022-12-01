using AutoMapper;
using CwkBooking.Api.Dto;
using CwkBooking.Domain.Abstractions.Services;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationGetDto>>> GetAllRerservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var toDto = _mapper.Map<List<ReservationGetDto>>(reservations);
            return toDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationGetDto>> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var toDto = _mapper.Map<ReservationGetDto>(reservation);
            return toDto;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationGetDto>> MakeReservation(ReservationPutPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var newReservation = await _reservationService.MakeReservationAsync(reservation);

            if (newReservation == null)
            {
                return BadRequest("Cannot create reservation");
            }

            var result = _mapper.Map<ReservationGetDto>(newReservation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveReservation(int id)
        {
            var reservation = await _reservationService.CancelReservationAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}