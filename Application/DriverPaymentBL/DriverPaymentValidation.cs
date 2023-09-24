using FluentValidation;
using Infrastructure.Dtos.DriverPaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverPaymentBL
{
    public class DriverPaymentValidation : AbstractValidator<PostDriverPaymentDto>
    {
        public DriverPaymentValidation()
        {

            RuleFor(x => x.PaidAmount).NotEmpty();
            RuleFor(x => x.PaymentType).NotEmpty();
            RuleFor(x => x.PaymentStatus).NotEmpty();

            RuleFor(x=>x.PaidAmount).Equal(x => x.TotalAmount);
        }
    }
}
