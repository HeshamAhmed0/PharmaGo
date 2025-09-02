using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls.OrderModuls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configrations
{
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(E => E.BuyerEmail).IsRequired();
            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.Property(a => a.FirstName).HasColumnName("FirstName").HasMaxLength(50);
                sa.Property(a => a.LastName).HasColumnName("LastName").HasMaxLength(50);
                sa.Property(a => a.Street).HasColumnName("Street").HasMaxLength(100);
                sa.Property(a => a.City).HasColumnName("City").HasMaxLength(50);
                sa.Property(a => a.Country).HasColumnName("Country").HasMaxLength(50);
            });
            builder.Property(o => o.DeliveryMethod)
             .HasConversion<string>();

            builder.HasOne(Id => Id.Customer)
                   .WithMany(o=>o.Orders)
                   .HasForeignKey(id => id.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.DeliveryMethod)
                .HasConversion<string>()
                .IsRequired();


            builder.Property(o => o.PaymentStatus)
                   .HasConversion<string>();
            
            builder.Property(o => o.Subtotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

        }
    }
}
