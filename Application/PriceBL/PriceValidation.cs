using FluentValidation;
using Infrastructure.Dtos.PriceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PriceBL
{
    public class PriceValidation : AbstractValidator<PostPriceDto>
    {
        public PriceValidation()
        {
            RuleFor(x => x.KM).NotEmpty();
            RuleFor(x => x.Prices).NotEmpty();
        }
    }
}
