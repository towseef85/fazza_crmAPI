using Domain.Drivers;
using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderPlaces
{
    public class OrderPlace
    {
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public Guid PriceId { get; set; }
        public virtual Price Price { get; set; }
        public string COD { get; set; }
        public string CODStatus { get; set; }
        public Guid DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
