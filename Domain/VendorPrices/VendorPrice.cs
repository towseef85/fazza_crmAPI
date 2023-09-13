using Domain.Common;
using Domain.Prices;
using Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VendorPrices
{
    public class VendorPrice: BaseEntity
    {
        public Guid VendorId { get; set; }
        public Guid PriceId { get; set; }
        //public virtual Vendor Vendors { get; set; }
        //public virtual Price Prices { get; set; }
    }
}
