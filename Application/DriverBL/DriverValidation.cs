using FluentValidation;
using Infrastructure.Dtos.DriverDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DriverBL
{
    public class DriverValidation : AbstractValidator<DriverDto>
    {
        public DriverValidation()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MobileNo).NotEmpty();
            RuleFor(x => x.WorkType).Must((x => x == "Full Time" || x == "Part Time"));
            RuleFor(x => x.Status).Must((x => x == "Active" || x == "Inactive"));
        }
    }

}

