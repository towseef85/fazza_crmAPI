
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.PriceDto
{
    public class PostPriceDto
    {
        public Guid Id { get; set; }
        public decimal KM { get; set; }
        public decimal Prices { get; set; }
        public bool IsActive { get; set; }

    }
}
