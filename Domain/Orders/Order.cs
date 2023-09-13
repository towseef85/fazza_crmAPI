using Domain.Common;
using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class Order:BaseEntity
    {
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public Guid PriceId { get; set; }
        public Price Price { get; set; }
        public string COD { get; set; }
        public string CODStatus { get; set; }
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
