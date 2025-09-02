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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.Property(o => o.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(oi => oi.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(oi => oi.Brand).IsRequired(false);
            builder.Property(oi => oi.Type).IsRequired(false);
            builder.Property(oi => oi.PictureUrl).IsRequired(false);

        }
    }

}
