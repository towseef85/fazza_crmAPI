using FluentValidation;
using Infrastructure.Dtos.VendorPriceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VendorPricesBL
{
    public class VendorPriceValidation:AbstractValidator<VendorPriceDto>
    {
        public VendorPriceValidation() {

            RuleFor(x => x.VendorId).NotNull().NotEmpty();
            RuleFor(x => x.PriceId).NotNull().NotEmpty();
        }
    }
}
