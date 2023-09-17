using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.DriverDto
{
    public class GetDriverDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string WorkType { get; set; }
        public string Status { get; set; }
        public Guid? CreatedUserId { get; set; }
    }
}
