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
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(D => D.DeliveryMethod)
                   .HasConversion<string>();
            builder.Property(P => P.PaymentStatus)
                   .HasConversion<string>();
            builder.HasOne(C => C.Customer)
                   .WithMany(O => O.Orders)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(OI=>OI.OrderItems)
                   .WithOne(O=>O.Order)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
