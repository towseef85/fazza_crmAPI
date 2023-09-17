using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.VendorDto
{
    public class GetVendorDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string OwnerName { get; set; }
        public string MobileNo { get; set; }
        public string? OfficeNumber { get; set; }
        public string EmailId { get; set; }
        public string? LocationUrl { get; set; }
        public string LeadSource { get; set; }
        public string PickupAddress { get; set; }
        public Guid? CreatedUserId { get; set; }
    }
}
