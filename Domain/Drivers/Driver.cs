using Domain.Common;
using Domain.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Drivers
{
    public class Driver:BaseEntity
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string WorkType { get; set; }
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
