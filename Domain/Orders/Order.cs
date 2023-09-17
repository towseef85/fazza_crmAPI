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
    public class Order : BaseEntity
    {

        public Guid VendorId { get; set; }
        public Guid PriceId { get; set; }
        public string? COD { get; set; }
        public string? CODStatus { get; set; }
         public Guid DriverId { get; set; }
        public DateTime? RecevingDate { get; set; }
        public string? TypePayment { get; set; }
        public string? Remarks { get; set; }

        public virtual Vendor Vendors { get; set; }
        public virtual Driver Drivers { get; set; }
        public virtual Price Prices { get; set; }
    }
}
