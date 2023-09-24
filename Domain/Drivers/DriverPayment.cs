using Domain.Common;
using Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Drivers
{
    public class DriverPayment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public string? PaymentType { get; set; }//Cash,Card,UpI
        public string? PaymentStatus { get; set; }//Paid,Unpaid,Others
        public string? Remarks { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Order Order { get; set; }


    }
}
