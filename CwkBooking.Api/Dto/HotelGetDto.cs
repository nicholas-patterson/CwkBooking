using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwkBooking.Api.Dto
{
    public class HotelGetDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}