using System;
using CwkBooking.Api.Services.Abstractions;

namespace CwkBooking.Api.Services
{
    public class TransientOperation : ITransientOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}

