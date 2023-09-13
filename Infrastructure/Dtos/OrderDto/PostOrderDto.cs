﻿using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.OrderDto
{
    public class PostOrderDto
    {
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public Guid PriceId { get; set; }
        public string COD { get; set; }
        public string CODStatus { get; set; }
        public Guid DriverId { get; set; }
    }
}
