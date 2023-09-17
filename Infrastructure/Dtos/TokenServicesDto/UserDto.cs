using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.TokenServicesDto
{
    public class UserDto
    {
        public string EmailId { get; set; }
        public string Password { get; set; }

        public static implicit operator UserDto(string v)
        {
            throw new NotImplementedException();
        }
    }
}
