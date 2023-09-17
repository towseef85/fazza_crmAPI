using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.VendorPriceDto
{
    public class GetVendorPriceDto
    {
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public Guid PriceId { get; set; }
        public Guid? CreatedUserId { get; set; }
    }
}
