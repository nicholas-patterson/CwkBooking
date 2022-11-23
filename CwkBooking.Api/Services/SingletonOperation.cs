using System;
using CwkBooking.Api.Services.Abstractions;

namespace CwkBooking.Api.Services
{
    public class SingletonOperation : ISingletonOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}

