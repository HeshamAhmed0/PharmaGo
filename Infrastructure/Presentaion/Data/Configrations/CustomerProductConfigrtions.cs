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
    public class CustomerProductConfigrtions : IEntityTypeConfiguration<CustomerProduct>
    {
        public void Configure(EntityTypeBuilder<CustomerProduct> builder)
        {
            builder.HasKey(FK => new{ FK.ProductId , FK.CustomerId});
            builder.HasOne(P => P.Product)
                   .WithMany(C => C.customerProducts)
                   .HasForeignKey(FK =>FK.ProductId);
            builder.HasOne(P => P.Customer)
                   .WithMany(C => C.customerProducts)
                   .HasForeignKey(FK =>FK.CustomerId);
            builder.Property(P => P.Quantity)
                   .IsRequired();
            builder.Property(AD => AD.AddedDate)
                   .HasDefaultValueSql("GETDATE()");

        }
    }
}
