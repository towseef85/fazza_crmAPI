using Domain.Common;
using Domain.Orders;
using Domain.Vendors;
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

        public ICollection<VendorPrice> VendorPrices { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
