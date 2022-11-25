using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CwkBooking.Api.Dto;
using CwkBooking.Domain.Models;

namespace CwkBooking.Api.Automapper
{
    public class HotelMappingProfiles : Profile
    {
        public HotelMappingProfiles()
        {
            CreateMap<HotelCreateDto, Hotel>();
            CreateMap<Hotel, HotelGetDto>();
        }
    }
}