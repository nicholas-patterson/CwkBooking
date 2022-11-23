using System;
using CwkBooking.Api.Services.Abstractions;

namespace CwkBooking.Api.Services
{
    public class ScopedOperation : IScopedOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}

