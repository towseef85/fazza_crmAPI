using FluentValidation;
using Infrastructure.Dtos.VendorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VendorBL
{
    public class VendorValidation:AbstractValidator<VendorDto>
    {
        public VendorValidation() {

            RuleFor(x => x.StoreName).NotEmpty();
            RuleFor(x => x.OwnerName).NotEmpty();
            RuleFor(x => x.MobileNo).NotEmpty();
            RuleFor(x => x.LeadSource).NotEmpty();
            RuleFor(x => x.EmailId).NotEmpty();
            RuleFor(x => x.PickupAddress).NotEmpty();
        }
    }
}
