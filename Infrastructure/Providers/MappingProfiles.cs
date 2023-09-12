using AutoMapper;
using Domain.Drivers;
using Domain.Vendors;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.VendorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<VendorDto, Vendor>();
            CreateMap<Vendor, GetVendorDto>();

            CreateMap<DriverDto, Driver>();
            CreateMap<Driver, GetDriverDto>();
        }
    }
}
