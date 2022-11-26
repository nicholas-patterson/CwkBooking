using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwkBooking.Api.Dto
{
    public class RoomPostPutDto
    {
        public int RoomNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedsRepair { get; set; }
    }
}