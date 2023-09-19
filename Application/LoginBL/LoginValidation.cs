using FluentValidation;
using Infrastructure.Dtos.LoginDto;

namespace Application.LoginBL
{
    public class LoginValidation : AbstractValidator<LoginUserDto>
    {
        public LoginValidation()
        {

            RuleFor(x => x.EmailId).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
