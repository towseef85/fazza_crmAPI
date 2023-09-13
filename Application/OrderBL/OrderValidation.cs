using Domain.Prices;
using Domain.Vendors;
using FluentValidation;
using Infrastructure.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderBL
{
    public class OrderValidation:AbstractValidator<PostOrderDto>
    {
        public OrderValidation() { 
        
            RuleFor(x=>x.VendorId).NotEmpty();
            RuleFor(x => x.PriceId).NotEmpty();
            RuleFor(x => x.COD).NotEmpty();
            RuleFor(x => x.CODStatus).NotEmpty();
            RuleFor(x => x.DriverId).NotEmpty();

        }
    }
}
