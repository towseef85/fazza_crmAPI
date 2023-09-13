using AutoMapper;
using Domain.Drivers;
using Domain.Prices;
using Domain.VendorPrices;
using Domain.Vendors;
using Infrastructure.Dtos.DriverDto;
using Infrastructure.Dtos.PriceDto;
using Infrastructure.Dtos.VendorDto;
using Infrastructure.Dtos.VendorPriceDto;
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
            CreateMap<VendorPriceDto, VendorPrice>();
            CreateMap<VendorPrice, GetVendorPriceDto>();

            CreateMap<PriceDto, Price>();
            CreateMap<Price, GetPriceDto>();

            CreateMap<VendorDto, Vendor>();
            CreateMap<Vendor, GetVendorDto>();

            CreateMap<DriverDto, Driver>();
            CreateMap<Driver, GetDriverDto>();
        }
    }
}
