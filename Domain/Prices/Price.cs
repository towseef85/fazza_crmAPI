using Domain.Common;
using Domain.VendorPrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prices
{
    public class Price:BaseEntity
    {
        public decimal KM { get; set; }
        public decimal Prices { get; set; }
        public bool IsActive { get; set; }
       // public ICollection<VendorPrice> vendorPrices { get; set; }
    }
}
