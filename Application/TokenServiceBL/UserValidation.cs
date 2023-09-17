using FluentValidation;
using Infrastructure.Dtos.TokenServicesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TokenServiceBL
{
    public class UserValidation : AbstractValidator<UserDto>
    {
        public UserValidation()
        {

            RuleFor(x => x.EmailId).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
