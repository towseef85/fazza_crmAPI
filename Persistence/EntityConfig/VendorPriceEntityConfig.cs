using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Vendors;

namespace Persistence.EntityConfig
{
    public class VendorPriceEntityConfig : IEntityTypeConfiguration<VendorPrice>
    {
        public void Configure(EntityTypeBuilder<VendorPrice> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(x => x.Prices)
            //    .WithMany(x => x.vendorPrices)
            //    .HasForeignKey(x => x.PriceId);

            //builder.HasOne(x => x.Vendors)
            //.WithMany(x => x.vendorPrices)
            //.HasForeignKey(x => x.Ve);
        }
    }
}
