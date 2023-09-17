using FluentValidation;
using Infrastructure.Dtos.AppUserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUserBL
{
    public class AppUserValdation:AbstractValidator<PostAppUserDto>
    {
        public AppUserValdation() { 
        
            RuleFor(x=>x.Email).NotEmpty().MaximumLength(85);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(25);
            RuleFor(x=>x.MobileNo).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(85);
        }
    }
}
