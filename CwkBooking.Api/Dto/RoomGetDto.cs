using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwkBooking.Api.Dto
{
    public class RoomGetDto
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedsRepair { get; set; }
    }
}