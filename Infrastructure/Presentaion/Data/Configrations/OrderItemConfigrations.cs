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
    public class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(P=>P.Product)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(O=>O.Order)
                   .WithMany(OI => OI.OrderItems)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(P => P.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
