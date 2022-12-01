using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwkBooking.Api.Dto
{
    public class ReservationGetDto
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public string Customer { get; set; }
    }
}