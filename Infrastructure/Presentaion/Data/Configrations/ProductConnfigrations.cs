using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configrations
{
    public class ProductConnfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(N => N.ProductName).IsRequired();
            builder.Property(PD => PD.ProductDescription).IsRequired();
            builder.Property(PURL => PURL.PictureUrl).IsRequired();
            builder.Property(P=>P.ProductPrice).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
