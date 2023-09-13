using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.VendorPriceDto
{
    public class PostVendorPriceDto
    {
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public Guid PriceId { get; set; }

    }
}
