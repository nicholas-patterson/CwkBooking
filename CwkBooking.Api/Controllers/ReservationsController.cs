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

        [HttpPost]
        public async Task<ActionResult<ReservationGetDto>> MakeReservation(ReservationPutPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var newReservation = await _reservationService.MakeReservation(reservation);

            if (newReservation == null)
            {
                return BadRequest("Cannot create reservation");
            }

            var result = _mapper.Map<ReservationGetDto>(newReservation);
            return result;
        }
    }
}