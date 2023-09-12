using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Domain
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
        public float Amount { get; set; }
    }
}
